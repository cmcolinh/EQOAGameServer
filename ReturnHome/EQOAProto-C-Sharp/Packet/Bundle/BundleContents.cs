using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle {
    public interface BundleContents : BinaryRecord {
        static Lazy<BundleContents> NoContents = new Lazy<BundleContents>(() => new BundleContents.Empty());
        ushort BundleNumber();
        byte BundleType();
        ushort BundleAcknowledged();
        ushort ReliableMessageAcknowledged();
        bool HasAcks();
        IList<BundleMessage> Messages();

        public static BundleContents Read(PacketBytes packetBytes) {
            if (packetBytes.Count == 0) {
                return BundleContents.NoContents.Value;
            }
            Uint8 bundleType = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            BundlePayload bundlePayload = BundlePayload.Read(packetBytes, selection: bundleType.ToByte());
            return new BundleContents.Impl(bundleType, bundlePayload);
        }

        public static BundleContents Of(byte bundleType, BundlePayload bundlePayload) => new BundleContents.Impl(
            bundleType: Uint8.Of(bundleType),
            bundlePayload: bundlePayload);

        private class Impl : BundleContents {
            readonly Uint8 bundleType;
            readonly BundlePayload bundlePayload;
            readonly Lazy<PacketBytes> bytes;

            public ushort BundleNumber() => bundlePayload.BundleNumber();
            public byte BundleType() => bundleType.ToByte();
            public ushort BundleAcknowledged() => bundlePayload.BundleAcknowledged();
            public ushort ReliableMessageAcknowledged() => bundlePayload.ReliableMessageAcknowledged();
            public bool HasAcks() => bundlePayload.HasAcks();
            public IList<BundleMessage> Messages() => bundlePayload.Messages();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(Uint8 bundleType, BundlePayload bundlePayload) {
                this.bundleType = bundleType;
                this.bundlePayload = bundlePayload;
                this.bytes = new Lazy<PacketBytes>(() => this.bundleType.Serialize()
                    .Append(this.bundlePayload.Serialize()));
            }
        }

        private class Empty : BundleContents {
            readonly PacketBytes bytes;
            public ushort BundleNumber() => 0;
            public byte BundleType() => 0xFF;
            public ushort BundleAcknowledged() => 0;
            public ushort ReliableMessageAcknowledged() => 0;
            public bool HasAcks() => false;
            public IList<BundleMessage> Messages() => new List<BundleMessage>();
            public PacketBytes Serialize() => bytes;
            public override string ToString() => "Empty";
            public Empty() {
                this.bytes = PacketBytes.Of(new List<byte>());
            }
        }
    }
}
