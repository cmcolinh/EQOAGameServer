using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface Uint24Le : BinaryRecord {
        uint ToUint();

        public static Uint24Le Read(PacketBytes packetBytes) => new Uint24Le.Impl((uint)((packetBytes[2] << 16) | (packetBytes[1] << 8) | packetBytes[0]));
        public static Uint24Le Of(uint val) => new Uint24Le.Impl(val);

        private class Impl : Uint24Le {
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
                    ((byte)(this.val >> 16))}));
            }
        }
    }
}
