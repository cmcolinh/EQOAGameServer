using ReturnHome.Packet.Bundle.Message.Types;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Message {
    public interface OpcodeMessage : BinaryRecord {
        void Accept(HandleMessage handleMessage);
        OpcodeAndMessage ToOpcodeAndMessage();

        private static readonly Dictionary<ushort, Func<PacketBytes, OpcodeMessage>> GetMessageTypeFor = new Dictionary<ushort, Func<PacketBytes, OpcodeMessage>> {
            {DiscVersion.OPCODE, packetBytes => DiscVersion.Read(packetBytes)},
            {AccountCredentials.OPCODE, packetBytes => AccountCredentials.Read(packetBytes)},
            {Message0x0e.OPCODE, packetBytes => Message0x0e.Read(packetBytes)},
            {Message0x14.OPCODE, packetBytes => Message0x14.Read(packetBytes)},
            {Message0xc9.OPCODE, packetBytes => Message0xc9.Read(packetBytes)},
            {Message0xca.OPCODE, packetBytes => Message0xca.Read(packetBytes)},
            {CharacterSelect.OPCODE, packetBytes => CharacterSelect.Read(packetBytes)},
            {CharacterCreation.OPCODE, packetBytes => CharacterCreation.Read(packetBytes)},
            {CharacterViewing.OPCODE, packetBytes => CharacterViewing.Read(packetBytes)},
            {CharacterDeletion.OPCODE, packetBytes => CharacterDeletion.Read(packetBytes)},
            {IntermediateTransferConnection.OPCODE, packetBytes => IntermediateTransferConnection.Read(packetBytes)},
            {IntermediateTransferConnectionAcknowledgement.OPCODE, packetBytes => IntermediateTransferConnectionAcknowledgement.Read(packetBytes)},
            {GameServerList.OPCODE, packetBytes => GameServerList.Read(packetBytes)},
            {AccountCredentials.ALTERNATE_OPCODE, packetBytes => AccountCredentials.Read(packetBytes)}
        };

        public static OpcodeMessage Read(PacketBytes packetBytes, ushort selection) {
            ushort opcode = selection;
            return GetMessageTypeFor[opcode](packetBytes);
        }
    }
}
