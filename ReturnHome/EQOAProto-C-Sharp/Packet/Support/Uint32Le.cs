using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface Uint32Le : BinaryRecord {
        uint ToUint();

        public static Uint32Le Read(PacketBytes packetBytes) => new Uint32Le.Impl((uint)(packetBytes[3] << 24 | packetBytes[2] << 16 | packetBytes[1] << 8 | packetBytes[0]));
        public static Uint32Le Of(uint val) => new Uint32Le.Impl(val);

        private class Impl : Uint32Le {
            readonly uint val;
            readonly Lazy<PacketBytes> bytes;

            public uint ToUint() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public Impl(uint val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{
                    ((byte)(this.val)),
                    ((byte)(this.val >> 8)),
                    ((byte)(this.val >> 16)),
                    ((byte)(this.val >> 24))}));
            }
        }
    }
}
