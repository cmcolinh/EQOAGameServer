using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface Uint16Le : MaybeUint16Le {
        private static readonly Lazy<MaybeUint16Le> noValue = new Lazy<MaybeUint16Le>(() => new Uint16Le.NoValue());
        static Uint16Le Read(PacketBytes packetBytes) => new Uint16Le.Impl((ushort)(packetBytes[1] << 8 | packetBytes[0]));
        static Uint16Le Of(ushort val) => new Uint16Le.Impl(val);
        static MaybeUint16Le NotRequired() => noValue.Value;

        private class Impl : Uint16Le {
            readonly ushort val;
            readonly Lazy<PacketBytes> bytes;

            public ushort ToUshort() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public Impl(ushort val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{
                    ((byte)(this.val)),
                    ((byte)(this.val >> 8))}));
            }
        }

        private class NoValue : MaybeUint16Le {
            public ushort ToUshort() => 0;
            public override string ToString() => "Not Required";
            public PacketBytes Serialize() => PacketBytes.Of(new List<byte>());
        }
    }
}
