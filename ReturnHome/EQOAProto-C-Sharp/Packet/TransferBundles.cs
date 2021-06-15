using ReturnHome.Packet;
using System.Collections.Generic;

namespace ReturnHome.Packet {
    public interface TransferBundles : PacketBundles {
        public static new TransferBundles Read(PacketBytes packetBytes) => new TransferBundles.Impl(TransferBundle.Read(packetBytes));

        private class Impl : TransferBundles {
            readonly TransferBundle transferBundle;

            public IList<PacketBundle> Bundles() => new List<PacketBundle>{transferBundle}.AsReadOnly();
            public PacketBytes Serialize() => transferBundle.Serialize();

            public Impl(TransferBundle transferBundle) {
                this.transferBundle = transferBundle;
            }
        }
    }
}
