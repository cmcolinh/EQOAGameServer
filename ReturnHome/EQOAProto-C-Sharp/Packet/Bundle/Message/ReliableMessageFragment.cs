using ReturnHome;
using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message {
    public interface ReliableMessageFragment : MessageContents {
        public static MessageContents Read(PacketBytes packetBytes) {
            MessageLength messageLength = MessageLength.Read(packetBytes);
            Uint16Le messageNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            PacketBytes messageFragment = packetBytes.PopFirst(bytes: messageLength.ToUshort());
            return new ReliableMessageFragment.Impl(messageLength, messageNumber, messageFragment);
        }

        public static ReliableMessageFragment Of(PacketBytes messageFragment, ushort messageNumber) => new ReliableMessageFragment.Impl(
            messageLength: MessageLength.Of((ushort)messageFragment.Count),
            messageNumber: Uint16Le.Of(messageNumber),
            messageFragment: messageFragment);

        private class Impl : ReliableMessageFragment {
            readonly MessageLength messageLength;
            readonly Uint16Le messageNumber;
            readonly PacketBytes messageFragment;
            readonly Lazy<PacketBytes> bytes;

            public ushort MessageNumber() => messageNumber.ToUshort();
            public ushort MessageLen() => messageLength.ToUshort();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handlePacket) => throw new NotSupportedException("Packet Handlers do not handle ReliableMessageFragment messages, as this is a Server only message");
            ///<summary> Use messageChannel 0xfa if this is not the last fragment in the full message, or 0xfb if it is </summary>
            public BundleMessage ToBundleMessage(byte messageChannel) => BundleMessage.Of(messageChannel: messageChannel, messageContents: this);

            public Impl(MessageLength messageLength, Uint16Le messageNumber, PacketBytes messageFragment) {
                this.messageLength = messageLength;
                this.messageNumber = messageNumber;
                this.messageFragment = messageFragment;
                this.bytes = new Lazy<PacketBytes>(() => messageLength.Serialize()
                    .Append(messageNumber.Serialize())
                    .Append(messageFragment));
            }
        }
    }
}
