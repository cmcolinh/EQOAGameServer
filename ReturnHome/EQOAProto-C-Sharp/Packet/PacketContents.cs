using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet {
	public interface PacketContents : BinaryRecord {
        ushort SourceEndpoint();
        ushort DestinationEndpoint();
        IList<PacketBundle> Bundles();

        public static PacketContents Read(PacketBytes packetBytes) {
            ushort firstTwoBytes = (ushort)(packetBytes[0]  + (packetBytes[1] << 8));
            return firstTwoBytes == 0xffff ? ReadTransferPacketContents(packetBytes) : ReadNormalPacketContents(packetBytes);
        }

        public static PacketContents Of(ushort sourceEndpoint, ushort destinationEndpoint, PacketBundles packetBundles) {
            EqoaPacketHeader eqoaPacketHeader = EqoaPacketHeader.Of(sourceEndpoint, destinationEndpoint);
            return new PacketContents.Impl(eqoaPacketHeader, packetBundles);
        }

        private static PacketContents ReadTransferPacketContents(PacketBytes packetBytes) {
            Uint8 transferPacketMarker = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            PacketBundles packetBundles = TransferBundles.Read(packetBytes);
            return new PacketContents.PacketTransfer(transferPacketMarker, packetBundles);
        }

        private static PacketContents ReadNormalPacketContents(PacketBytes packetBytes) {
            EqoaPacketHeader eqoaPacketHeader = EqoaPacketHeader.Read(packetBytes.PopFirst(bytes: 4));
            PacketBundles packetBundles = PacketBundles.Read(packetBytes);
            return new PacketContents.Impl(eqoaPacketHeader, packetBundles);
        }

        private class Impl : PacketContents {
            readonly EqoaPacketHeader eqoaPacketHeader;
            readonly PacketBundles packetBundles;
            readonly Lazy<PacketBytes> bytes;

            public ushort SourceEndpoint() => eqoaPacketHeader.SourceEndpoint();
            public ushort DestinationEndpoint() => eqoaPacketHeader.DestinationEndpoint();
            public IList<PacketBundle> Bundles() => packetBundles.Bundles();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(EqoaPacketHeader eqoaPacketHeader, PacketBundles packetBundles) {
                this.eqoaPacketHeader = eqoaPacketHeader;
                this.packetBundles = packetBundles;
                this.bytes = new Lazy<PacketBytes>(() => this.eqoaPacketHeader.Serialize()
                    .Append(this.packetBundles.Serialize()));
            }
        }

        private class PacketTransfer : PacketContents {
            readonly Uint8 transferPacketMarker;
            readonly PacketBundles packetBundles;
            readonly Lazy<PacketBytes> bytes;

            public IList<PacketBundle> Bundles() => packetBundles.Bundles();
            public ushort SourceEndpoint() => 0xffff;
            public ushort DestinationEndpoint() => 0xffff;
            public PacketBytes Serialize() => bytes.Value;

            public PacketTransfer(Uint8 transferPacketMarker, PacketBundles packetBundles) {
                this.transferPacketMarker = transferPacketMarker;
                this.packetBundles = packetBundles;
                this.bytes = new Lazy<PacketBytes>(() => transferPacketMarker.Serialize()
                    .Append(packetBundles.Serialize()));
            }
        }
    }
}
