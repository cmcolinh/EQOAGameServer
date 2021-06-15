using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Types {
    public class NewProcessMessages : BundlePayload {
        public static readonly byte TYPE_OF = 0x00;

        public static BundlePayload Read(PacketBytes packetBytes) {
            Uint16Le bundleNumber = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            BundleMessages messages = BundleMessages.Read(packetBytes);
            return new NewProcessMessages(bundleNumber, messages);
        }

        readonly Uint16Le bundleNumber;
        readonly BundleMessages messages;
        readonly Lazy<PacketBytes> bytes;

        public ushort BundleNumber() => bundleNumber.ToUshort();
        public ushort BundleAcknowledged() => 0;
        public ushort ReliableMessageAcknowledged() => 0;
        public BundleContents ToBundleContents() => BundleContents.Of(bundleType: NewProcessMessages.TYPE_OF, bundlePayload: this);
        public bool HasAcks() => false;
        public IList<BundleMessage> Messages() => messages.Messages();
        public PacketBytes Serialize() => bytes.Value;

        private NewProcessMessages(Uint16Le bundleNumber, BundleMessages messages) {
            this.bundleNumber = bundleNumber;
            this.messages = messages;
            this.bytes = new Lazy<PacketBytes>(() => this.bundleNumber.Serialize().Append(this.messages.Serialize()));
        }
    }
}
