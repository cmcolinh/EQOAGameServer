using ReturnHome;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface Message0x14 : OpcodeMessage {
        public static readonly ushort OPCODE = 0x0014;

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            return new Message0x14.Impl();
        }

        private class Impl : Message0x14 {
            public Impl() {
            }

            public PacketBytes Serialize() => PacketBytes.Of(new List<byte>());
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessMessage0x14(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: Message0x14.OPCODE, opcodeMessage: this);
        }
    }
}
