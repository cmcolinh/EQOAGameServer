using ReturnHome;
using ReturnHome.Packet.Support;
using ReturnHome.Packet.Bundle.Message;
using System;

namespace ReturnHome.Packet.Bundle {
    public interface BundleMessage : BinaryRecord {
        ushort MessageNumber();
        byte MessageChannel();
        void Accept(HandleMessage handleMessage);

        public static BundleMessage Read(PacketBytes packetBytes) {
            Uint8 messageChannel = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            MessageContents messageContents = MessageContents.Read(packetBytes, selection: messageChannel.ToByte());
            return new BundleMessage.Impl(messageChannel, messageContents);
        }

        public static BundleMessage Of(byte messageChannel, MessageContents messageContents) {
            Uint8 type = Uint8.Of(messageChannel);
            return new BundleMessage.Impl(type, messageContents);
        }

        private class Impl : BundleMessage {
            readonly Uint8 messageChannel;
            readonly MessageContents messageContents;
            readonly Lazy<PacketBytes> bytes;

            public ushort MessageNumber() => messageContents.MessageNumber();
            public byte MessageChannel() => messageChannel.ToByte();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) {
                messageContents.Accept(handleMessage);
            }

            public Impl(Uint8 messageChannel, MessageContents messageContents) {
                this.messageChannel = messageChannel;
                this.messageContents = messageContents;
                this.bytes = new Lazy<PacketBytes>(() => this.messageChannel.Serialize()
                    .Append(this.messageContents.Serialize()));
            }
        }
    }
}
