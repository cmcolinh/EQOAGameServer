using ReturnHome;
using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message {
    public interface ReliableMessage : MessageContents {
        public static readonly byte TYPE_OF = 0xfb;
        public static MessageContents Read(PacketBytes packetBytes) {
            MessageLength messageLength = MessageLength.Read(packetBytes);
            Uint16Le messageNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            OpcodeAndMessage opcodeAndMessage = OpcodeAndMessage.Read(packetBytes.PopFirst(bytes: messageLength.ToUshort()));
            return new ReliableMessage.Impl(messageLength, messageNumber, opcodeAndMessage);
        }

        public static ReliableMessage Of(OpcodeAndMessage opcodeAndMessage, ushort messageNumber) => new ReliableMessage.Impl(
                messageLength: MessageLength.Of((ushort)opcodeAndMessage.Serialize().Count),
                messageNumber: Uint16Le.Of(messageNumber),
                opcodeAndMessage: opcodeAndMessage);

        private class Impl : ReliableMessage {
            readonly MessageLength messageLength;
            readonly Uint16Le messageNumber;
            readonly OpcodeAndMessage opcodeAndMessage;
            readonly Lazy<PacketBytes> bytes;

            public ushort MessageNumber() => messageNumber.ToUshort();
            public ushort MessageLen() => messageLength.ToUshort();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handlePacket) => opcodeAndMessage.Accept(handlePacket);
            public BundleMessage ToBundleMessage(byte messageChannel = 0xfb) => BundleMessage.Of(messageChannel: messageChannel, messageContents: this);

            public Impl(MessageLength messageLength, Uint16Le messageNumber, OpcodeAndMessage opcodeAndMessage) {
                this.messageLength = messageLength;
                this.messageNumber = messageNumber;
                this.opcodeAndMessage = opcodeAndMessage;
                this.bytes = new Lazy<PacketBytes>(() => this.messageLength.Serialize()
                    .Append(this.messageNumber.Serialize())
                    .Append(this.opcodeAndMessage.Serialize()));
            }
        }
    }
}
