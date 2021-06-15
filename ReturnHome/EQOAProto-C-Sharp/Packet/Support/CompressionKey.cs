using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface CompressionKey : BinaryRecord {
        private static Lazy<CompressionKey> sentinel = new Lazy<CompressionKey>(() => new CompressionKey.Sentinel());
        public static CompressionKey SentinelValue() => sentinel.Value;

        byte NonZeroBytesToTake();
        byte ZeroBytesToCompress();
        bool IsSentinel();

        static CompressionKey Read(PacketBytes packetBytes) {
            byte firstByte = packetBytes.PopFirst(bytes: 1)[0];
            if (firstByte == 0x00) {
                return CompressionKey.SentinelValue();
            } else if (firstByte < 0x80) {
                return new CompressionKey.OfOneByte(
                    nonZeroBytesToTake: (byte)(firstByte >> 4),
                    zeroBytesToCompress: (byte)(firstByte & 0x0F));
            } else {
                byte zeroBytesToCompress = packetBytes.PopFirst(bytes: 1)[0];
                return new CompressionKey.OfTwoBytes(
                    nonZeroBytesToTake: (byte)(firstByte & 0x7F),
                    zeroBytesToCompress: zeroBytesToCompress);
            }
        }

        static CompressionKey Of(byte zeroBytesToCompress, byte nonZeroBytesToTake) {
            if (zeroBytesToCompress < 16 && nonZeroBytesToTake < 8) {
                return new CompressionKey.OfOneByte(
                    zeroBytesToCompress: zeroBytesToCompress,
                    nonZeroBytesToTake: nonZeroBytesToTake);
            } else {
                return new CompressionKey.OfTwoBytes(
                    zeroBytesToCompress: zeroBytesToCompress,
                    nonZeroBytesToTake: nonZeroBytesToTake);
            }
        }

        private class OfOneByte : CompressionKey {
            readonly byte zeroBytesToCompress;
            readonly byte nonZeroBytesToTake;
            readonly Lazy<PacketBytes> bytes;

            public byte NonZeroBytesToTake() => nonZeroBytesToTake;
            public byte ZeroBytesToCompress() => zeroBytesToCompress;
            public bool IsSentinel() => false;
            public PacketBytes Serialize() => bytes.Value;

            public OfOneByte(byte nonZeroBytesToTake, byte zeroBytesToCompress) {
                this.nonZeroBytesToTake = nonZeroBytesToTake;
                this.zeroBytesToCompress = zeroBytesToCompress;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{(byte)((nonZeroBytesToTake << 4) + zeroBytesToCompress)}));
            }
        }

        private class OfTwoBytes : CompressionKey {
            readonly byte zeroBytesToCompress;
            readonly byte nonZeroBytesToTake;
            readonly Lazy<PacketBytes> bytes;

            public byte NonZeroBytesToTake() => nonZeroBytesToTake;
            public byte ZeroBytesToCompress() => zeroBytesToCompress;
            public bool IsSentinel() => false;
            public PacketBytes Serialize() => bytes.Value;

            public OfTwoBytes(byte nonZeroBytesToTake, byte zeroBytesToCompress) {
                this.nonZeroBytesToTake = nonZeroBytesToTake;
                this.zeroBytesToCompress = zeroBytesToCompress;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{(byte)(0x80 | nonZeroBytesToTake), zeroBytesToCompress}));
            }
        }

        private class Sentinel : CompressionKey {
            readonly Lazy<PacketBytes> bytes;
            public byte NonZeroBytesToTake() => 0;
            public byte ZeroBytesToCompress() => 0;
            public bool IsSentinel() => true;
            public PacketBytes Serialize() => bytes.Value;

            public Sentinel() {
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{0x00}));
            }
        }
    }
}
