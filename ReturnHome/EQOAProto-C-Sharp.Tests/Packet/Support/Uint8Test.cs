using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class Uint8Test {
        public Uint8Test() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{128});
            byte actualValue = Uint8.Read(packetBytes).ToByte();
            uint expectedValue = (byte)128;
            Assert.True(expectedValue == actualValue);
        }

        [Fact]
        public void TestInitializeFromByte() {
            Uint8 val = Uint8.Of((byte) 88);
            PacketBytes expected = PacketBytes.Of(new List<byte>{(byte) 88});
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
