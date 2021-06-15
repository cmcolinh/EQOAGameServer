using ReturnHome;
using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface Message0xc9 : OpcodeMessage {
        public static readonly ushort OPCODE = 0x00c9;
        ushort Val();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint16Le val = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            return new Message0xc9.Impl(val);
        }

        private class Impl : Message0xc9 {
            readonly Uint16Le val;

            public Impl(Uint16Le val) {
                this.val = val;
            }

            public ushort Val() => val.ToUshort();
            public PacketBytes Serialize() => val.Serialize();
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessMessage0xc9(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: Message0xc9.OPCODE, opcodeMessage: this);
        }
    }
}
