using System;

namespace ReturnHome.Packet.Support {
    public interface MessageLength : BinaryRecord {
        ushort ToUshort();
        static MessageLength Read(PacketBytes packetBytes) {
            Uint8 len = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            MaybeUint16Le extendedLen = len.ToByte() == 0xFF ? Uint16Le.Read(packetBytes.PopFirst(bytes: 2)) : Uint16Le.NotRequired();
            return new MessageLength.Impl(len, extendedLen);
        }

        static MessageLength Of(ushort messageLength) {
            Uint8 len = messageLength > 254 ? Uint8.Of(0xff) : Uint8.Of((byte)messageLength);
            MaybeUint16Le extendedLen = messageLength > 254 ? Uint16Le.Of(messageLength) : Uint16Le.NotRequired();
            return new MessageLength.Impl(len, extendedLen);
        }

        private class Impl : MessageLength {
            readonly Uint8 len;
            readonly MaybeUint16Le extendedLen;
            readonly Lazy<PacketBytes> bytes;

            public ushort ToUshort() => extendedLen.ToUshort() == 0 ? len.ToByte() : extendedLen.ToUshort();
            public new string ToString() => $"{ToUshort()}";
            public PacketBytes Serialize() => bytes.Value;

            public Impl(Uint8 len, MaybeUint16Le extendedLen) {
                this.len = len;
                this.extendedLen = extendedLen;
                this.bytes = new Lazy<PacketBytes>(() => len.Serialize()
                    .Append(extendedLen.Serialize()));
            }
        }
    }
}
