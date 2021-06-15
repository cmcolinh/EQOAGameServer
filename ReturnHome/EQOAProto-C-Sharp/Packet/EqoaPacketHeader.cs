using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet {
    public interface EqoaPacketHeader : BinaryRecord {
        ushort SourceEndpoint();
        ushort DestinationEndpoint();
        public static EqoaPacketHeader Read(PacketBytes packetBytes) {
            Uint16Le sourceEndpoint = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le destinationEndpoint = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            return new EqoaPacketHeader.Impl(sourceEndpoint, destinationEndpoint);
        }

        public static EqoaPacketHeader Of(ushort sourceEndpoint, ushort destinationEndpoint) {
            return new EqoaPacketHeader.Impl(
                Uint16Le.Of(sourceEndpoint), Uint16Le.Of(destinationEndpoint));
        }

        private class Impl : EqoaPacketHeader {
            readonly Uint16Le sourceEndpoint;
            readonly Uint16Le destinationEndpoint;
            readonly Lazy<PacketBytes> bytes;

            public ushort SourceEndpoint() => sourceEndpoint.ToUshort();
            public ushort DestinationEndpoint() => destinationEndpoint.ToUshort();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(Uint16Le sourceEndpoint, Uint16Le destinationEndpoint) {
                this.sourceEndpoint = sourceEndpoint;
                this.destinationEndpoint = destinationEndpoint;
                this.bytes = new Lazy<PacketBytes>(() => sourceEndpoint.Serialize()
                    .Append(destinationEndpoint.Serialize()));
            }
        }
    }
}
