using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public interface ProcessUpdateReport : BundlePayload {
        public static readonly byte TYPE_OF = 0x13;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastBundleAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastMessageAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            UpdateMessageAcks updateMessageAck = UpdateMessageAcks.Read(packetBytes);
            BundleMessages messages = BundleMessages.Read(packetBytes);
            return new ProcessUpdateReport.Impl(bundleNumber, lastBundleAck, lastMessageAck, updateMessageAck);
        }

        private class Impl : ProcessUpdateReport {
            readonly Uint16Le bundleNumber;
            readonly Uint16Le lastBundleAck;
            readonly Uint16Le lastMessageAck;
            readonly UpdateMessageAcks updateMessageAcks;
            readonly Lazy<PacketBytes> bytes;

            public ushort BundleNumber() => bundleNumber.ToUshort();
            public ushort BundleAcknowledged() => lastBundleAck.ToUshort();
            public ushort ReliableMessageAcknowledged() => lastMessageAck.ToUshort();
            public BundleContents ToBundleContents() => BundleContents.Of(bundleType: ProcessUpdateReport.TYPE_OF, bundlePayload: this);
            public bool HasAcks() => true;
            public IList<BundleMessage> Messages() => BundleMessages.Empty().Messages();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(Uint16Le bundleNumber, Uint16Le lastBundleAck, Uint16Le lastMessageAck, UpdateMessageAcks updateMessageAcks) {
                this.bundleNumber = bundleNumber;
                this.lastBundleAck = lastBundleAck;
                this.lastMessageAck = lastMessageAck;
                this.updateMessageAcks = updateMessageAcks;
                this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize()
                    .Append(this.lastBundleAck.Serialize())
                    .Append(this.lastMessageAck.Serialize())
                    .Append(this.updateMessageAcks.Serialize()));
            }
        }
    }
}
