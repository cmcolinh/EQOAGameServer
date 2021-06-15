using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CompressedUpdateMessage : BinaryRecord {
        PacketBytes Decompress();
        private static Lazy<CompressedUpdateMessage> sentinel = new Lazy<CompressedUpdateMessage>(() => new CompressedUpdateMessage.Sentinel());
        public static CompressedUpdateMessage SentinelValue() => sentinel.Value;
        public static CompressedUpdateMessage Read(PacketBytes packetBytes) {
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            if (compressionKey.IsSentinel()) { //sentinel value
                return CompressedUpdateMessage.SentinelValue();
            }
            PacketBytes keptXorBytes = packetBytes.PopFirst(bytes: compressionKey.NonZeroBytesToTake());
            CompressedUpdateMessage remainderOfMessage = CompressedUpdateMessage.Read(packetBytes); //repeat same process until you have the whole message
            return new CompressedUpdateMessage.Impl(compressionKey, keptXorBytes, remainderOfMessage);
        }

        public static CompressedUpdateMessage Compress(PacketBytes xorBytes) {
            List<byte> takenBytes = new List<byte>();
            byte zeroBytesToCompress = 0;
            while (xorBytes.Count > 0) {
                byte nextByte = xorBytes.PopFirst(bytes: 1)[0];
                if (nextByte != 0x00) {
                    takenBytes.Add(nextByte);
                    while (xorBytes.Count > 0) {
                        nextByte = xorBytes[0];
                        if (nextByte == 0x00) {
                            return new CompressedUpdateMessage.Impl(
                                compressionKey: CompressionKey.Of(zeroBytesToCompress: zeroBytesToCompress, nonZeroBytesToTake: (byte)takenBytes.Count),
                                keptXorBytes: PacketBytes.Of(takenBytes),
                                remainderOfMessage: CompressedUpdateMessage.Compress(xorBytes));
                        }
                        takenBytes.Add(xorBytes.PopFirst(bytes: 1)[0]);
                    }
                } else {
                    zeroBytesToCompress++;
                }
            }
            return new CompressedUpdateMessage.Impl(
                compressionKey: CompressionKey.Of(zeroBytesToCompress: zeroBytesToCompress, nonZeroBytesToTake: (byte)takenBytes.Count),
                keptXorBytes: PacketBytes.Of(takenBytes),
                remainderOfMessage: CompressedUpdateMessage.SentinelValue());
        }

        private class Impl : CompressedUpdateMessage {
            readonly CompressionKey compressionKey;
            readonly PacketBytes keptXorBytes;
            readonly CompressedUpdateMessage remainderOfMessage;
            readonly Lazy<PacketBytes> bytes;
            readonly Lazy<PacketBytes> decompressedBytes;

            public PacketBytes Serialize() => bytes.Value;
            public PacketBytes Decompress() => decompressedBytes.Value;

            public Impl(CompressionKey compressionKey, PacketBytes keptXorBytes, CompressedUpdateMessage remainderOfMessage) {
                this.compressionKey = compressionKey;
                this.keptXorBytes = keptXorBytes;
                this.remainderOfMessage = remainderOfMessage;
                this.bytes = new Lazy<PacketBytes>(() => compressionKey.Serialize()
                    .Append(keptXorBytes)
                    .Append(remainderOfMessage.Serialize()));
                this.decompressedBytes = new Lazy<PacketBytes>(() => DecompressedBytes());
            }

            private PacketBytes DecompressedBytes() {
                List<byte> decompressedZeros = new List<byte>();
                decompressedZeros.AddRange(Enumerable.Repeat((byte)0x00, (int)compressionKey.ZeroBytesToCompress()));
                return PacketBytes.Of(decompressedZeros)
                    .Append(keptXorBytes)
                    .Append(remainderOfMessage.Decompress());
            }
        }

        private class Sentinel : CompressedUpdateMessage {
            private Lazy<PacketBytes> bytes;
            private Lazy<PacketBytes> decompressedBytes;

            public PacketBytes Serialize() => bytes.Value;
            public PacketBytes Decompress() => decompressedBytes.Value;

            public Sentinel() {
                this.bytes = new Lazy<PacketBytes>(() => CompressionKey.SentinelValue().Serialize()); //just a single 0x00
                this.decompressedBytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>())); //decompresses to nothing
            }
        }
    }
}
