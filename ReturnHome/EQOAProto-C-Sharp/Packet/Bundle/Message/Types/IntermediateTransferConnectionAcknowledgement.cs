using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface IntermediateTransferConnectionAcknowledgement : OpcodeMessage {
        public static readonly ushort OPCODE = 0x0993;
        uint TransferNumber();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint32Le transferNumber = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new IntermediateTransferConnectionAcknowledgement.Impl(transferNumber);
        }

        public static OpcodeMessage Of(uint transferNumber) => new IntermediateTransferConnectionAcknowledgement.Impl(Uint32Le.Of(transferNumber));

        private class Impl : IntermediateTransferConnectionAcknowledgement {
            readonly Uint32Le transferNumber;

            public Impl(Uint32Le transferNumber) {
                this.transferNumber = transferNumber;
            }

            public uint TransferNumber() => transferNumber.ToUint();
            public PacketBytes Serialize() => transferNumber.Serialize();
            public void Accept(HandleMessage handleMessage) => throw new NotSupportedException($"Packet Handlers do not handle {typeof(IntermediateTransferConnectionAcknowledgement).Name} messages, as this is a Server only message");
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: IntermediateTransferConnectionAcknowledgement.OPCODE, opcodeMessage: this);
        }
    }
}
