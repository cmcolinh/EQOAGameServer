using System;

namespace ReturnHome.Packet.Support {
    public interface UpdateMessageAck : BinaryRecord {
        byte Channel();
        ushort MessageNum();

        public static UpdateMessageAck Read(PacketBytes packetBytes) {
            Uint8 channel = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            MaybeUint16Le messageNumber = channel.ToByte() == 0xf8 ? Uint16Le.NotRequired() : Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            return new UpdateMessageAck.Impl(channel, messageNumber);
        }

        public static UpdateMessageAck Of(byte channel, ushort messageNumber) => new UpdateMessageAck.Impl(
            channel: Uint8.Of(channel),
            messageNumber: Uint16Le.Of(messageNumber));

        private class Impl : UpdateMessageAck {
            readonly Uint8 channel;
            readonly MaybeUint16Le messageNumber;
            readonly Lazy<PacketBytes> bytes;

            public byte Channel() => channel.ToByte();
            public ushort MessageNum() => messageNumber.ToUshort();
            public PacketBytes Serialize() => bytes.Value;
            public override string ToString() => channel.ToByte() == 0xf8 ? "End of unreliable acks marker (0xf8)" : $"Ack channel 0x{channel.ToByte().ToString("x2")}, message {messageNumber.ToUshort()}";

            public Impl(Uint8 channel, MaybeUint16Le messageNumber) {
                this.channel = channel;
                this.messageNumber = messageNumber;
                this.bytes = new Lazy<PacketBytes>(() => channel.Serialize()
                    .Append(messageNumber.Serialize()));
            }
        }
    }
}
