using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class ProcessAll : BundlePayload {
        public static readonly byte TYPE_OF = 0x63;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint32Le sessionIdAck = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastBundleAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le lastMessageAck = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            BundleMessages messages = BundleMessages.Read(packetBytes);
            return new ProcessAll(sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, messages);
        }

        public static BundlePayload Of(uint sessionIdAck, ushort bundleNumber, ushort lastBundleAck, ushort lastMessageAck, List<BundleMessage> bundleMessages) => new ProcessAll(
            sessionIdAck: Uint32Le.Of(sessionIdAck),
            bundleNumber: Uint16Le.Of(bundleNumber),
            lastBundleAck: Uint16Le.Of(lastBundleAck),
            lastMessageAck: Uint16Le.Of(lastMessageAck),
            messages: BundleMessages.Of(bundleMessages));

        readonly Uint32Le sessionIdAck;
        readonly Uint16Le bundleNumber;
        readonly Uint16Le lastBundleAck;
        readonly Uint16Le lastMessageAck;
        readonly BundleMessages messages;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => lastBundleAck.ToUshort();
        public ushort ReliableMessageAcknowledged() => lastMessageAck.ToUshort();
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: ProcessAll.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => true;
        public IList<BundleMessage> Messages() => messages.Messages();
        public PacketBytes Serialize() => bytes.Value;

        private ProcessAll(Uint32Le sessionIdAck, Uint16Le bundleNumber, Uint16Le lastBundleAck, Uint16Le lastMessageAck, BundleMessages messages) {
            this.sessionIdAck = sessionIdAck;
            this.bundleNumber = bundleNumber;
            this.lastBundleAck = lastBundleAck;
            this.lastMessageAck = lastMessageAck;
            this.messages = messages;
            this.bytes = new Lazy<PacketBytes>(() => this.sessionIdAck.Serialize()
                .Append(this.bundleNumber.Serialize())
                .Append(this.lastBundleAck.Serialize())
                .Append(this.lastMessageAck.Serialize())
                .Append(this.messages.Serialize()));
        }
    }
}
