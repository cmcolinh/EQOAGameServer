using Xunit;
using System;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class Uint64LeTest {
        public Uint64LeTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x72, 0x59, 0x32, 0x13, 0x35, 0x01, 0x00, 0x00});
            ulong actual = Uint64Le.Read(packetBytes).ToUlong();
            ulong expected = (ulong)0x0000013513325972; //is little endian so the order should be reversed
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestReadTimestampFromPacketBytes() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x04, 0x03, 0x02, 0x01, 0x37, 0x01, 0x00, 0x00});
            DateTimeOffset actual = Uint64Le.Read(packetBytes).ToDateTimeOffset();
            DateTimeOffset expected = DateTimeOffset.FromUnixTimeMilliseconds(0x0000013701020304); //is little endian so the order should be reversed
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromUlong() {
            Uint64Le val = Uint64Le.Of((ulong)0x000001351346f2e2);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0xe2, 0xf2, 0x46, 0x13, 0x35, 0x01, 0x00, 0x00}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInitializeFromDateTimeOffset() {
            DateTimeOffset timestamp = DateTimeOffset.FromUnixTimeMilliseconds(0x0000013601020304);
            Uint64Le val = Uint64Le.Of(timestamp);
            PacketBytes expected = PacketBytes.Of(new List<byte>{0x04, 0x03, 0x02, 0x01, 0x36, 0x01, 0x00, 0x00}); //is little endian so the order should be reversed
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
