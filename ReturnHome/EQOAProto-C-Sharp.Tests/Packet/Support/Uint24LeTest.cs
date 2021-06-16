using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class Uint24LeTest {
        public Uint24LeTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x64, 0x13, 0x81});
            uint actual = Uint24Le.Read(packetBytes).ToUint();
            uint expected = 0x811364; //is little endian so the order should be reversed
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromUint() {
            Uint24Le val = Uint24Le.Of(0x445566);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0x66, 0x55, 0x44}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
