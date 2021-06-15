using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle {
    public interface BundleMessages : BinaryRecord {
        IList<BundleMessage> Messages();

        public static BundleMessages Read(PacketBytes packetBytes) {
            List<BundleMessage> messages = new List<BundleMessage>();
            while(packetBytes.Count > 0) {
                messages.Add(BundleMessage.Read(packetBytes));
            }
            return new BundleMessages.Impl(messages);
        }

        public static BundleMessages Of(List<BundleMessage> messages) => new BundleMessages.Impl(messages);

        public static BundleMessages Empty() {
            return new BundleMessages.Impl(new List<BundleMessage>());
        }

        private class Impl : BundleMessages {
            readonly IList<BundleMessage> messages;
            readonly Lazy<PacketBytes> bytes;

            public IList<BundleMessage> Messages() => messages;
            public PacketBytes Serialize() => bytes.Value;

            public Impl(List<BundleMessage> messages) {
                this.messages = messages.AsReadOnly();
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                PacketBytes result = PacketBytes.Of(new List<byte>());
                foreach (BundleMessage message in this.messages) {result = result.Append(message.Serialize());}
                return result;
            }
        }
    }
}
