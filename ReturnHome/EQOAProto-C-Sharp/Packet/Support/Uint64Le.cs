using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface Uint64Le : BinaryRecord {
        ulong ToUlong();
        DateTimeOffset ToDateTimeOffset();

        public static Uint64Le Read(PacketBytes packetBytes) => new Uint64Le.Impl((ulong)((ulong)packetBytes[7] << 56 | (ulong)packetBytes[6] << 48 | (ulong)packetBytes[5] << 40 | (ulong)packetBytes[4] << 32 | (ulong)packetBytes[3] << 24 | (ulong)packetBytes[2] << 16 | (ulong)packetBytes[1] << 8 | packetBytes[0]));
        public static Uint64Le Of(ulong val) => new Uint64Le.Impl(val);
        public static Uint64Le Of(DateTimeOffset timestamp) => Uint64Le.Of((ulong)timestamp.ToUnixTimeMilliseconds());

        private class Impl : Uint64Le {
            readonly ulong val;
            readonly Lazy<PacketBytes> bytes;

            public ulong ToUlong() => val;
            public DateTimeOffset ToDateTimeOffset() => DateTimeOffset.FromUnixTimeMilliseconds((long)val);
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public Impl(ulong val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{
                    ((byte)(this.val)),
                    ((byte)(this.val >> 8)),
                    ((byte)(this.val >> 16)),
                    ((byte)(this.val >> 24)),
                    ((byte)(this.val >> 32)),
                    ((byte)(this.val >> 40)),
                    ((byte)(this.val >> 48)),
                    ((byte)(this.val >> 56))}));
            }
        }
    }
}
