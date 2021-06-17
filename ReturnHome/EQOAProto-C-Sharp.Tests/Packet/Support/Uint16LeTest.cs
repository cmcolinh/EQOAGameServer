using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class Uint16LeTest {
        public Uint16LeTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x12, 0x34});
            ushort actual = Uint16Le.Read(packetBytes).ToUshort();
            ushort expected = 0x3412; //is little endian so the order should be reversed
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromUshort() {
            Uint16Le val = Uint16Le.Of(0x9876);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0x76, 0x98}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
