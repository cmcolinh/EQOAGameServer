using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class ASCIIStringTest {
        public ASCIIStringTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{(byte)'H', (byte)'e', (byte)'l', (byte)'l', (byte)'o'});
            string actualValue = ASCIIString.Read(packetBytes).ToString();
            string expectedValue = "Hello";
            Assert.True(expectedValue == actualValue);
        }

        [Fact]
        public void TestInitializeFromString() {
            ASCIIString val = ASCIIString.Of("Guard Devin");
            PacketBytes expected = PacketBytes.Of(new List<byte>{(byte)'G', (byte)'u', (byte)'a', (byte)'r', (byte)'d', (byte)' ', (byte)'D', (byte)'e', (byte)'v', (byte)'i', (byte)'n'});
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
