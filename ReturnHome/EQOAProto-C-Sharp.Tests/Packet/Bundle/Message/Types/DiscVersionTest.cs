using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Bundle.Message;
using Packet.Bundle.Message.Types;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Bundle.Message.Types {
    public class DiscVersionTest {
        [Fact]
        public void TestReadDiscVersion() {
            List<byte> bytes = new List<byte> {0x25, 0x00, 0x00, 0x00};
            PacketBytes expectedBytes = PacketBytes.Of(bytes);
            OpcodeMessage discVersion = DiscVersion.Read(PacketBytes.Of(bytes));
            PacketBytes actualBytes = discVersion.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void TestReadDiscVersionData() {
            List<byte> bytes = new List<byte> {0x25, 0x00, 0x00, 0x00};
            uint expectedVersion = 0x25;
            DiscVersion discVersion = (DiscVersion)DiscVersion.Read(PacketBytes.Of(bytes));
            uint actualVersion = discVersion.Version();
            Assert.Equal(expectedVersion, actualVersion);
        }

        [Fact]
        public void TestBuildOpcodeAndMessage() {
            List<byte> opcodeMessageBytes = new List<byte>{0x25, 0x00, 0x00, 0x00};
            List<byte> opcodeAndMessageBytes = new List<byte>{0x00, 0x00, 0x25, 0x00, 0x00, 0x00};
            PacketBytes expectedBytes = PacketBytes.Of(opcodeAndMessageBytes);
            OpcodeMessage discVersion = DiscVersion.Read(PacketBytes.Of(opcodeMessageBytes));
            OpcodeAndMessage opcodeAndMessage = discVersion.ToOpcodeAndMessage();
            PacketBytes actualBytes = opcodeAndMessage.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
