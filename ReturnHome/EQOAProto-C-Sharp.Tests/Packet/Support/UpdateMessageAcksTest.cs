using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class UpdateMessageAcksTest {
        [Fact]
        public void TestReadUpdateMessageAcks() {
            List<byte> bytes = new List<byte> { //from Packet 277
                0x00, 0x03, 0x00, 0x01, 0x01, 0x00, 0x02, 0x01, 0x00, 0x42, 0x01, 0x00, 0x43, 0x01, 0x00, 0xf8
            };
            PacketBytes packetBytes = PacketBytes.Of(bytes);
            UpdateMessageAcks updateMessageAcks = UpdateMessageAcks.Read(packetBytes);
            int expectedCount = 5;
            int actualCount = updateMessageAcks.UpdateMessageAcks().Count;
            Assert.Equal(expectedCount, actualCount);
        }
    }
}
