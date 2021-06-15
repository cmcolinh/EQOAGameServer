using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class Uint32LeTest {
        public Uint32LeTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x12, 0x34, 0x56, 0x78});
            uint actual = Uint32Le.Read(packetBytes).ToUint();
            uint expected = 0x78563412; //is little endian so the order should be reversed
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromUint() {
            Uint32Le val = Uint32Le.Of(0x14393001);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0x01, 0x30, 0x39, 0x14}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
