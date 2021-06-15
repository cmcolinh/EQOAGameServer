using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface Uint8 : BinaryRecord {
        byte ToByte();

        static Uint8 Read(PacketBytes packetBytes) => new Uint8.Impl(packetBytes[0]);

        static Uint8 Of(byte val) => new Uint8.Impl(val);

        private class Impl : Uint8 {
            readonly byte val;
            readonly Lazy<PacketBytes> bytes;

            public byte ToByte() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public Impl(byte val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{val}));
            }
        }
    }
}
