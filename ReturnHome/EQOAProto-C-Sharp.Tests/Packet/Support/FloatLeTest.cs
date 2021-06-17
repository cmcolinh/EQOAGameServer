using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class FloatLeTest {
        public FloatLeTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x46, 0xc5, 0xec, 0x5c});
            float actual = FloatLe.Read(packetBytes).ToFloat();
            float expected = 25334.18F;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromFloat() {
            FloatLe val = FloatLe.Of(-1.5462527F);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0xbf, 0xc5, 0xeb, 0x9c}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
