using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class ProcessReport : BundlePayload {
        public static readonly byte TYPE_OF = 0x23;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastBundleAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastMessageAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            return new ProcessReport(bundleNumber, lastBundleAck, lastMessageAck);
        }

        public static BundlePayload Of(ushort bundleNumber, ushort lastBundleAck, ushort lastMessageAck) => new ProcessReport(
            bundleNumber: Uint16Le.Of(bundleNumber),
            lastBundleAck: Uint16Le.Of(lastBundleAck),
            lastMessageAck: Uint16Le.Of(lastMessageAck));

        readonly Uint16Le bundleNumber;
        readonly Uint16Le lastBundleAck;
        readonly Uint16Le lastMessageAck;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => lastBundleAck.ToUshort();
        public ushort ReliableMessageAcknowledged() => lastMessageAck.ToUshort();
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: ProcessReport.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => true;
        public IList<BundleMessage> Messages() => BundleMessages.Empty().Messages();
        public PacketBytes Serialize() => bytes.Value;

        private ProcessReport(Uint16Le bundleNumber, Uint16Le lastBundleAck, Uint16Le lastMessageAck) {
            this.bundleNumber = bundleNumber;
            this.lastBundleAck = lastBundleAck;
            this.lastMessageAck = lastMessageAck;
            this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize()
                .Append(this.lastBundleAck.Serialize())
                .Append(this.lastMessageAck.Serialize()));
        }
    }
}
