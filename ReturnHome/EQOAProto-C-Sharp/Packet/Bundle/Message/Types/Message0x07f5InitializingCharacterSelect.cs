using ReturnHome;
using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface Message0x07f5InitializingCharacterSelect : OpcodeMessage {
        public static readonly ushort OPCODE = 0x07f5;
        uint Value();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint32Le value = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new Message0x07f5InitializingCharacterSelect.Impl(value);
        }

        public static OpcodeMessage Of(uint value) => new Message0x07f5InitializingCharacterSelect.Impl(Uint32Le.Of(value));

        private class Impl : Message0x07f5InitializingCharacterSelect {
            readonly Uint32Le value;

            public Impl(Uint32Le value) {
                this.value = value;
            }

            public uint Value() => value.ToUint();
            public PacketBytes Serialize() => value.Serialize();
            public void Accept(HandleMessage handleMessage) => throw new NotSupportedException($"The server cannot handle {this.GetType()} messages from client");
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: Message0x07f5InitializingCharacterSelect.OPCODE, opcodeMessage: this);
        }
    }
}
