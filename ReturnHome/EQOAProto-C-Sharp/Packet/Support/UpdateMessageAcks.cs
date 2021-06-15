using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface UpdateMessageAcks : BinaryRecord {
        private static readonly Lazy<UpdateMessageAcks> noValue = new Lazy<UpdateMessageAcks>(() => new UpdateMessageAcks.NoValue());
        static UpdateMessageAcks NoMoreAcks() => noValue.Value;
        IList<UpdateMessageAck> UpdateMessageAcks();

        public static UpdateMessageAcks Read(PacketBytes packetBytes) {
            UpdateMessageAck updateMessageAck = UpdateMessageAck.Read(packetBytes);
            UpdateMessageAcks remainingAcks = updateMessageAck.Channel() == 0xf8 ? NoMoreAcks() : Read(packetBytes);
            return new UpdateMessageAcks.Impl(updateMessageAck, remainingAcks);
        }

        private class Impl : UpdateMessageAcks {
            readonly UpdateMessageAck updateMessageAck;
            readonly UpdateMessageAcks remainingAcks;
            readonly Lazy<PacketBytes> bytes;

            public IList<UpdateMessageAck> UpdateMessageAcks() {
                if (updateMessageAck.Channel() == 0xf8) {
                    return new List<UpdateMessageAck>().AsReadOnly();
                } else {
                    List<UpdateMessageAck> updateMessageAcks = new List<UpdateMessageAck>{updateMessageAck};
                    updateMessageAcks.AddRange(remainingAcks.UpdateMessageAcks());
                    return updateMessageAcks.AsReadOnly();
                }
            }
            public PacketBytes Serialize() => bytes.Value;

            public Impl(UpdateMessageAck updateMessageAck, UpdateMessageAcks remainingAcks) {
                this.updateMessageAck = updateMessageAck;
                this.remainingAcks = remainingAcks;
                this.bytes = new Lazy<PacketBytes>(() => this.updateMessageAck.Serialize()
                    .Append(remainingAcks.Serialize()));
            }
        }

        private class NoValue : UpdateMessageAcks {
            public IList<UpdateMessageAck> UpdateMessageAcks() => new List<UpdateMessageAck>().AsReadOnly();
            public PacketBytes Serialize() => PacketBytes.Of(new List<byte>());
        }
    }
}
