using ReturnHome;
using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message {
    public interface UnreliableMessage : MessageContents {
        public static readonly byte TYPE_OF = 0xfc;
        public static MessageContents Read(PacketBytes packetBytes) {
            MessageLength messageLength = MessageLength.Read(packetBytes);
            OpcodeAndMessage opcodeAndMessage = OpcodeAndMessage.Read(packetBytes.PopFirst(bytes: messageLength.ToUshort()));
            return new UnreliableMessage.Impl(messageLength, opcodeAndMessage);
        }

        public static UnreliableMessage Of(OpcodeAndMessage opcodeAndMessage) => new UnreliableMessage.Impl(
                messageLength: MessageLength.Of((ushort)opcodeAndMessage.Serialize().Count),
                opcodeAndMessage: opcodeAndMessage);

        private class Impl : UnreliableMessage {
            readonly MessageLength messageLength;
            readonly OpcodeAndMessage opcodeAndMessage;
            readonly Lazy<PacketBytes> bytes;

            public ushort MessageNumber() => 0;
            public ushort MessageLen() => messageLength.ToUshort();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => opcodeAndMessage.Accept(handleMessage);
            public BundleMessage ToBundleMessage(byte messageChannel = 0xfc) => BundleMessage.Of(messageChannel: messageChannel, messageContents: this);

            public Impl(MessageLength messageLength, OpcodeAndMessage opcodeAndMessage) {
                this.messageLength = messageLength;
                this.opcodeAndMessage = opcodeAndMessage;
                this.bytes = new Lazy<PacketBytes>(() => this.messageLength.Serialize()
                    .Append(this.opcodeAndMessage.Serialize()));
            }
        }
    }
}
