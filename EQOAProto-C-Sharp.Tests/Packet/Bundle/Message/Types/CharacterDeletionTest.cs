using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Bundle.Message;
using Packet.Bundle.Message.Types;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Bundle.Message.Types {
    public class CharacterDeletionTest {
        [Fact]
        public void TestReadCharacterDeletion() {
            List<byte> bytes = new List<byte> {0x90, 0xa0, 0x8b, 0x01};
            PacketBytes expectedBytes = PacketBytes.Of(bytes);
            OpcodeMessage characterDeletion = CharacterDeletion.Read(PacketBytes.Of(bytes));
            PacketBytes actualBytes = characterDeletion.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void TestReadCharacterDeletionData() {
            List<byte> bytes = new List<byte> {0x90, 0xa0, 0x8b, 0x01};
            uint expectedEntityId = 0x116808; //the bytes are in VariableLength format, the value is a regular integer
            CharacterDeletion characterDeletion = (CharacterDeletion)CharacterDeletion.Read(PacketBytes.Of(bytes));
            uint actualEntityId = characterDeletion.EntityId();
            Assert.Equal(expectedEntityId, actualEntityId);
        }
    }
}
