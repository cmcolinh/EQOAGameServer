using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class NewProcessReport : BundlePayload {
        public static readonly byte TYPE_OF = 0x03;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastBundleAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastMessageAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            return new NewProcessReport(bundleNumber, lastBundleAck, lastMessageAck);
        }

        readonly Uint16Le bundleNumber;
        readonly Uint16Le lastBundleAck;
        readonly Uint16Le lastMessageAck;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => lastBundleAck.ToUshort();
        public ushort ReliableMessageAcknowledged() => lastMessageAck.ToUshort();
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: NewProcessReport.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => true;
        public IList<BundleMessage> Messages() => BundleMessages.Empty().Messages();
        public PacketBytes Serialize() => bytes.Value;

        private NewProcessReport(Uint16Le bundleNumber, Uint16Le lastBundleAck, Uint16Le lastMessageAck) {
            this.bundleNumber = bundleNumber;
            this.lastBundleAck = lastBundleAck;
            this.lastMessageAck = lastMessageAck;
            this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize()
                .Append(this.lastBundleAck.Serialize())
                .Append(this.lastMessageAck.Serialize()));
        }
    }
}
