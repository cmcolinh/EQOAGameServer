using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface FloatLe : BinaryRecord {
        float ToFloat();

        public static FloatLe Read(PacketBytes packetBytes) {
            float val = System.BitConverter.ToSingle(new byte[]{packetBytes[3], packetBytes[2], packetBytes[1], packetBytes[0]});
            return new FloatLe.Impl(val, packetBytes.PopFirst(bytes: 4));
        }
        public static FloatLe Of(float val) {
            byte[] bytes = BitConverter.GetBytes(val);
            byte[] leBytes = new byte[]{bytes[3], bytes[2], bytes[1], bytes[0]};
            return new FloatLe.Impl(val, PacketBytes.Of(new List<byte>(leBytes)));
        }

        private class Impl : FloatLe {
            readonly float val;
            readonly PacketBytes packetBytes;

            public float ToFloat() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => packetBytes;

            public Impl(float val, PacketBytes packetBytes) {
                this.val = val;
                this.packetBytes = packetBytes;
            }
        }
    }
}
