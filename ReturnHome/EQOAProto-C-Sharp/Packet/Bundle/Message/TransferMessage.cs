using ReturnHome;
using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message {
    public interface TransferMessage : MessageContents {
        public static readonly byte TYPE_OF = 0xff;
        public static MessageContents Read(PacketBytes packetBytes) => new TransferMessage.Impl(OpcodeAndMessage.Read(packetBytes));

        public static TransferMessage Of(OpcodeAndMessage opcodeAndMessage) => new TransferMessage.Impl(opcodeAndMessage);

        private class Impl : TransferMessage {
            readonly OpcodeAndMessage opcodeAndMessage;

            public ushort MessageNumber() => 0;
            public ushort MessageLen() => (ushort)opcodeAndMessage.Serialize().Count;
            public PacketBytes Serialize() => opcodeAndMessage.Serialize();
            public void Accept(HandleMessage handlePacket) => opcodeAndMessage.Accept(handlePacket);
            public BundleMessage ToBundleMessage(byte messageChannel = 0xff) => BundleMessage.Of(messageChannel: messageChannel, messageContents: this);

            public Impl(OpcodeAndMessage opcodeAndMessage) {
                this.opcodeAndMessage = opcodeAndMessage;
            }
        }
    }
}
