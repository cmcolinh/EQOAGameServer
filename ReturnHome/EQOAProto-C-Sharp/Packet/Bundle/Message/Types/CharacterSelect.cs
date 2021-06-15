using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CharacterSelect : OpcodeMessage {
        public static readonly ushort OPCODE = 0x002a;
        uint EntityId();
        uint FacePersonalization();
        uint HairStylePersonalization();
        uint HairLengthPersonalization();
        uint HairColorPersonalization();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint32Le entityId = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint32Le facePersonalization = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint32Le hairStylePersonalization = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint32Le hairLengthPersonalization = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint32Le hairColorPersonalization = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new CharacterSelect.Impl(entityId, facePersonalization, hairStylePersonalization, hairLengthPersonalization, hairColorPersonalization);
        }

        private class Impl : CharacterSelect {
            readonly Uint32Le entityId;
            readonly Uint32Le facePersonalization;
            readonly Uint32Le hairStylePersonalization;
            readonly Uint32Le hairLengthPersonalization;
            readonly Uint32Le hairColorPersonalization;
            readonly Lazy<PacketBytes> bytes;

            public Impl(Uint32Le entityId, Uint32Le facePersonalization, Uint32Le hairStylePersonalization, Uint32Le hairLengthPersonalization, Uint32Le hairColorPersonalization) {
                this.entityId = entityId;
                this.facePersonalization = facePersonalization;
                this.hairStylePersonalization = hairStylePersonalization;
                this.hairLengthPersonalization = hairLengthPersonalization;
                this.hairColorPersonalization = hairColorPersonalization;
                this.bytes = new Lazy<PacketBytes>(() => this.entityId.Serialize()
                    .Append(this.facePersonalization.Serialize())
                    .Append(this.hairStylePersonalization.Serialize())
                    .Append(this.hairLengthPersonalization.Serialize())
                    .Append(this.hairColorPersonalization.Serialize()));
            }

            public uint EntityId() => entityId.ToUint();
            public uint FacePersonalization() => facePersonalization.ToUint();
            public uint HairStylePersonalization() => hairStylePersonalization.ToUint();
            public uint HairLengthPersonalization() => hairStylePersonalization.ToUint();
            public uint HairColorPersonalization() => hairColorPersonalization.ToUint();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessCharacterSelect(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: CharacterSelect.OPCODE, opcodeMessage: this);
        }
    }
}
