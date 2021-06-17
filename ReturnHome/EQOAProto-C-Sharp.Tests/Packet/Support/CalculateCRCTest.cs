using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class CalculateCRCTest {
        private readonly CalculateCRC calculateCRC;

        public CalculateCRCTest() {
            this.calculateCRC = CalculateCRC.Instance.Value;
        }

        [Fact]
        //Matt's PCAP 156 (CRC is 0x9eb6697c, that is, 0x7c69b69e little endian)
        public void testPacket156() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{
                0xb0, 0x73, 0x5a, 0xe7, 0x95, 0x60, 0x5a, 0xe7, 0x05, 0x00, 0x63, 0x5a, 0xe7, 0x05, 0x00, 0x01,
                0x00, 0x01, 0x00, 0x02, 0x00, 0xfb, 0x06, 0x01, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00, 0x00});
            uint crcValue = calculateCRC.Calculate(packetBytes);
            uint expectedValue = (uint)0x9eb6697c;
            Assert.True(crcValue == expectedValue);
        }
    }
}
