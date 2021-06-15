using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CharacterDeletion : OpcodeMessage {
        public static readonly ushort OPCODE = 0x002d;
        uint EntityId();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            VariableLengthEncodedInt entityId = VariableLengthEncodedInt.Read(packetBytes.PopFirst(bytes: 4));
            return new CharacterDeletion.Impl(entityId);
        }

        private class Impl : CharacterDeletion {
            readonly VariableLengthEncodedInt entityId;

            public Impl(VariableLengthEncodedInt entityId) => this.entityId = entityId;

            public uint EntityId() => (uint)entityId.ToLong();
            public PacketBytes Serialize() => entityId.Serialize();
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessCharacterDeletion(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: CharacterDeletion.OPCODE, opcodeMessage: this);
        }
    }
}
