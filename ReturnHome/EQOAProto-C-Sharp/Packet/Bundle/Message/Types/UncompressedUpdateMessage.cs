using System.Collections.Generic;
using System.Linq;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface UncompressedUpdateMessage : BinaryRecord {
        private static readonly UncompressedUpdateMessage empty = new UncompressedUpdateMessage.Empty();
        public CompressedUpdateMessage CompressThisMessage() {
            return CompressThisMessage(with: empty);
        }

        public CompressedUpdateMessage CompressThisMessage(UncompressedUpdateMessage with) {
            UncompressedUpdateMessage compressionBaseMessage = with;
            PacketBytes serializedBytes = this.Serialize();
            PacketBytes baseMessageBytes = compressionBaseMessage.Serialize();

            List<byte> xorBytes = new List<byte>();
            for (int index = 0; index < serializedBytes.Count; index++) {
                xorBytes.Add((byte)(serializedBytes.ElementAt(index) ^ baseMessageBytes.ElementAtOrDefault(index)));
            }

            return CompressedUpdateMessage.Compress(PacketBytes.Of(xorBytes));
        }

        private class Empty: UncompressedUpdateMessage {
            public PacketBytes Serialize() => PacketBytes.Of(new List<byte>());

            public PacketBytes Decompress() => PacketBytes.Of(new List<byte>());

            public Empty() {
            }
        }
    }
}
