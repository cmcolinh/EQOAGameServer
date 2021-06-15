using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface DiscVersion : OpcodeMessage {
        public static readonly uint FRONTIERS = 0x25;
        public static readonly ushort OPCODE = 0x0000;
        uint Version();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint32Le version = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new DiscVersion.Impl(version);
        }

        public static OpcodeMessage Of(uint version) => new DiscVersion.Impl(Uint32Le.Of(version));

        private class Impl : DiscVersion {
            readonly Uint32Le version;

            public Impl(Uint32Le version) {
                this.version = version;
            }

            public uint Version() => version.ToUint();
            public PacketBytes Serialize() => version.Serialize();
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessDiscVersion(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: DiscVersion.OPCODE, opcodeMessage: this);
        }
    }
}
