using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class ProcessMessagesAndReport : BundlePayload {
        public static readonly byte TYPE_OF = 0x0d;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint32Le sessionIdAck = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastBundleAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastMessageAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            BundleMessages messages = BundleMessages.Read(packetBytes);
            return new ProcessMessagesAndReport(bundleNumber, lastBundleAck, lastMessageAck, messages);
        }

        public static BundlePayload Of(ushort bundleNumber, ushort lastBundleAck, ushort lastMessageAck, List<BundleMessage> bundleMessages) => new ProcessMessagesAndReport(
                bundleNumber: Uint16Le.Of(bundleNumber),
                lastBundleAck: Uint16Le.Of(lastBundleAck),
                lastMessageAck: Uint16Le.Of(lastMessageAck),
                messages: BundleMessages.Of(bundleMessages));

        readonly Uint16Le bundleNumber;
        readonly Uint16Le lastBundleAck;
        readonly Uint16Le lastMessageAck;
        readonly BundleMessages messages;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => lastBundleAck.ToUshort();
        public ushort ReliableMessageAcknowledged() => lastMessageAck.ToUshort();
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: ProcessMessagesAndReport.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => true;
        public IList<BundleMessage> Messages() => messages.Messages();
        public PacketBytes Serialize() => bytes.Value;

        private ProcessMessagesAndReport(Uint16Le bundleNumber, Uint16Le lastBundleAck, Uint16Le lastMessageAck, BundleMessages messages) {
            this.bundleNumber = bundleNumber;
            this.lastBundleAck = lastBundleAck;
            this.lastMessageAck = lastMessageAck;
            this.messages = messages;
            this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize()
                .Append(this.lastBundleAck.Serialize())
                .Append(this.lastMessageAck.Serialize())
                .Append(this.messages.Serialize()));
        }
    }
}
