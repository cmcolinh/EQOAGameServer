using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class ProcessMessages : BundlePayload {
        public static readonly byte TYPE_OF = 0x20;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            BundleMessages messages = BundleMessages.Read(packetBytes);
            return new ProcessMessages(bundleNumber, messages);
        }

        public static BundlePayload Of(ushort bundleNumber, List<BundleMessage> bundleMessages) => new ProcessMessages(
            bundleNumber: Uint16Le.Of(bundleNumber),
            messages: BundleMessages.Of(bundleMessages));

        readonly Uint16Le bundleNumber;
        readonly BundleMessages messages;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => 0;
        public ushort ReliableMessageAcknowledged() => 0;
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: ProcessMessages.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => false;
        public IList<BundleMessage> Messages() => messages.Messages();
        public PacketBytes Serialize() => bytes.Value;

        private ProcessMessages(Uint16Le bundleNumber, BundleMessages messages) {
            this.bundleNumber = bundleNumber;
            this.messages = messages;
            this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize().Append(this.messages.Serialize()));
        }
    }
}
