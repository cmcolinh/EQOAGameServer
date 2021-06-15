using ReturnHome;
using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface Message0x0e : OpcodeMessage {
        public static readonly ushort OPCODE = 0x000e;
        byte Val();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint8 val = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            return new Message0x0e.Impl(val);
        }

        private class Impl : Message0x0e {
            readonly Uint8 val;

            public Impl(Uint8 val) {
                this.val = val;
            }

            public byte Val() => val.ToByte();
            public PacketBytes Serialize() => val.Serialize();
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessMessage0x0e(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: Message0x0e.OPCODE, opcodeMessage: this);
        }
    }
}
