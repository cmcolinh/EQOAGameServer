using ReturnHome.Packet;
using ReturnHome.Packet.Bundle;
using System.Collections.Generic;

namespace ReturnHome.Packet {
    public interface TransferBundle : PacketBundle {
        public static new TransferBundle Read(PacketBytes packetBytes) => new TransferBundle.Impl(BundleContents.Read(packetBytes));

        private class Impl : TransferBundle {
            readonly BundleContents bundleContents;

            public ushort BundleNumber() => 0;
            public byte BundleType() => 0xff;
            public IList<BundleMessage> Messages() => bundleContents.Messages();
            public byte SessionAction() => 0xff;
            public ushort BundleAcknowledged() => 0;
            public ushort ReliableMessageAcknowledged() => 0;
            public bool HasAcks() => false;
            public bool SessionAckRequested() => false;
            public uint SessionIdBase() => 0;
            public uint SessionIdUp() => 0;
            public PacketBytes Serialize() => bundleContents.Serialize();

            public Impl(BundleContents bundleContents) {
                this.bundleContents = bundleContents;
            }
        }
    }
}
