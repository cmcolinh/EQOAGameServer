using ReturnHome.Packet.Bundle;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet {
    /// <summary>
    /// this interface is a model for a single bundle in a packet (most packets have just one bundle)
    /// providing accessors to the messages and metadata contained in this bundle
    /// </summary>
    public interface PacketBundle : BinaryRecord {
        static readonly byte SESSION_CLOSE = 0x14;
        ushort BundleNumber();
        byte SessionAction();
        byte BundleType();
        ushort BundleAcknowledged();
        ushort ReliableMessageAcknowledged();
        bool HasAcks();
        bool SessionAckRequested();
        uint SessionIdBase();
        uint SessionIdUp();
        IList<BundleMessage> Messages();

        public static PacketBundle Read(PacketBytes packetBytes) {
            BundleHeader bundleHeader = BundleHeader.Read(packetBytes);
            BundleContents bundleContents;
            if (bundleHeader.SessionAction() == SESSION_CLOSE) {
                bundleContents = SessionClose.Read(packetBytes.PopFirst(bytes: 4));
            } else {
                bundleContents = BundleContents.Read(packetBytes.PopFirst(bytes: bundleHeader.BundleLength()));
            }
            return new PacketBundle.Impl(bundleHeader, bundleContents);
        }

        public static PacketBundle Of(BundleHeader bundleHeader, BundleContents bundleContents) {
            return new PacketBundle.Impl(bundleHeader, bundleContents);
        }

        private class Impl : PacketBundle {
            readonly BundleHeader bundleHeader;
            readonly BundleContents bundleContents;
            readonly Lazy<PacketBytes> bytes;

            public ushort BundleNumber() => bundleContents.BundleNumber();
            public byte SessionAction() => bundleHeader.SessionAction();
            public byte BundleType() => bundleContents.BundleType();
            public ushort BundleAcknowledged() => bundleContents.BundleAcknowledged();
            public ushort ReliableMessageAcknowledged() => bundleContents.ReliableMessageAcknowledged();
            public bool HasAcks() => bundleContents.HasAcks();
            public bool SessionAckRequested() => (SessionAction() & 0x20) == 0x20;
            public uint SessionIdBase() => bundleHeader.SessionIdBase();
            public uint SessionIdUp() => bundleHeader.SessionIdUp();
            public IList<BundleMessage> Messages() => bundleContents.Messages();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(BundleHeader bundleHeader, BundleContents bundleContents) {
                this.bundleHeader = bundleHeader;
                this.bundleContents = bundleContents;
                this.bytes = new Lazy<PacketBytes>(() => this.bundleHeader.Serialize()
                    .Append(this.bundleContents.Serialize()));
            }
        }
    }
}
