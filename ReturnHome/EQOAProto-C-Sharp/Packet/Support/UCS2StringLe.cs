using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnHome.Packet.Support {
    public interface UCS2StringLe : BinaryRecord {
        public static UCS2StringLe Read(PacketBytes packetBytes) {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < packetBytes.Count; i = i + 2) {
                s.Append((char) packetBytes[i]);
            }
            return new UCS2StringLe.Impl(s.ToString());
        }

        public static UCS2StringLe Of(string val) => new UCS2StringLe.Impl(val);

        private class Impl : UCS2StringLe {
            readonly string val;
            readonly Lazy<PacketBytes> bytes;

            public override string ToString() => val;
            public PacketBytes Serialize() => bytes.Value;

            public Impl(string val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                PacketBytes bytes = PacketBytes.Of(new List<byte>());
                foreach (char c in val) {bytes = bytes.Append(PacketBytes.Of(new List<byte>{(byte)c, (byte)0}));}
                return bytes;
            }
        }
    }
}
