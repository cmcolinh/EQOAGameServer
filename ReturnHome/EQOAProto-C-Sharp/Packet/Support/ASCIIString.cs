using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnHome.Packet.Support {
    public interface ASCIIString : BinaryRecord {
        public static ASCIIString Read(PacketBytes packetBytes) {
            StringBuilder s = new StringBuilder();
            foreach (byte b in packetBytes) {
                s.Append((char) b);
            }
            return new ASCIIString.Impl(s.ToString());
        }

        public static ASCIIString Of(string val) => new ASCIIString.Impl(val);

        private class Impl : ASCIIString {
            readonly string val;
            readonly Lazy<PacketBytes> bytes;

            public override string ToString() => val;
            public PacketBytes Serialize() => bytes.Value;

            public Impl(string val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                List<byte> bytes = new List<byte>();
                foreach(char c in val){bytes.Add((byte) c);}
                return PacketBytes.Of(bytes);
            }
        }
    }
}
