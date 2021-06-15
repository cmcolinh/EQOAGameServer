using System;
using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message {
    public interface OpcodeAndMessage : BinaryRecord {
        void Accept(HandleMessage handleMessage);
        public static OpcodeAndMessage Read(PacketBytes packetBytes) {
            Uint16Le opcode = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            OpcodeMessage opcodeMessage = OpcodeMessage.Read(packetBytes, selection: opcode.ToUshort());
            return new OpcodeAndMessage.Impl(opcode, opcodeMessage);
        }

        public static OpcodeAndMessage Of(ushort opcode, OpcodeMessage opcodeMessage) => new OpcodeAndMessage.Impl(Uint16Le.Of(opcode), opcodeMessage);

        private class Impl : OpcodeAndMessage {
            readonly Uint16Le opcode;
            readonly OpcodeMessage opcodeMessage;
            readonly Lazy<PacketBytes> bytes;

            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => opcodeMessage.Accept(handleMessage);

            public Impl(Uint16Le opcode, OpcodeMessage opcodeMessage) {
                this.opcode = opcode;
                this.opcodeMessage = opcodeMessage;
                this.bytes = new Lazy<PacketBytes>(() => this.opcode.Serialize()
                    .Append(this.opcodeMessage.Serialize()));
            }
        }
    }
}
