using ReturnHome;
using ReturnHome.Packet.Support;
using ReturnHome.Packet.Bundle.Message.Types;
using System;

namespace ReturnHome.Packet.Bundle.Message {
    public interface UpdateMessage : MessageContents {
        public static MessageContents Read(PacketBytes packetBytes) {
            MessageLength messageLength = MessageLength.Read(packetBytes);
            Uint16Le messageNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint8 xorDelta = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            CompressedUpdateMessage compressedUpdateMessage = CompressedUpdateMessage.Read(packetBytes);
            return new UpdateMessage.Impl(
                messageLength: messageLength,
                messageNumber: messageNumber,
                xorDelta: xorDelta,
                compressedUpdateMessage: compressedUpdateMessage);
        }

        public static UpdateMessage Of(ushort messageLength, ushort messageNumber, byte xorDelta, CompressedUpdateMessage compressedUpdateMessage) {
            return new UpdateMessage.Impl(
                messageLength: MessageLength.Of(messageLength),
                messageNumber: Uint16Le.Of(messageNumber),
                xorDelta: Uint8.Of(xorDelta),
                compressedUpdateMessage: compressedUpdateMessage);
        }

        private class Impl : UpdateMessage {
            readonly MessageLength messageLength;
            readonly Uint16Le messageNumber;
            readonly Uint8 xorDelta;
            readonly CompressedUpdateMessage compressedUpdateMessage;
            readonly Lazy<PacketBytes> bytes;

            public ushort MessageNumber() => messageNumber.ToUshort();
            public ushort MessageLen() => messageLength.ToUshort();
            public void Accept(HandleMessage handleMessage) => handleMessage.HandleUpdateMessage(this);
            public PacketBytes Serialize() => bytes.Value;
            public BundleMessage ToBundleMessage(byte messageChannel) => BundleMessage.Of(messageChannel: messageChannel, messageContents: this);
            public PacketBytes DecompressedMessage() => compressedUpdateMessage.Decompress();

            public Impl(MessageLength messageLength, Uint16Le messageNumber, Uint8 xorDelta, CompressedUpdateMessage compressedUpdateMessage) {
                this.messageLength = messageLength;
                this.messageNumber = messageNumber;
                this.xorDelta = xorDelta;
                this.compressedUpdateMessage = compressedUpdateMessage;
                this.bytes = new Lazy<PacketBytes>(() => messageLength.Serialize()
                    .Append(messageNumber.Serialize())
                    .Append(xorDelta.Serialize())
                    .Append(compressedUpdateMessage.Serialize()));
            }
        }
    }
}
