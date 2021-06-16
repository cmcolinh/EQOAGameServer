using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class UpdateMessageAckTest {
        public UpdateMessageAckTest() {}

        [Theory]
        [InlineData(0x00, 0x01, 0x00, 0)]
        [InlineData(0x00, 0x02, 0x00, 0)]
        [InlineData(0x00, 0x00, 0x01, 0)]
        [InlineData(0x01, 0x01, 0x00, 1)]
        [InlineData(0x12, 0x01, 0x01, 18)]
        [InlineData(0xf8, 0x01, 0x00, 0xf8)]
        public void TestReadChannelFromPacketBytes(byte firstByte, byte secondByte, byte thirdByte, byte expectedValue) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            byte actualValue = UpdateMessageAck.Read(packetBytes).Channel();
            Assert.True(expectedValue == actualValue);
        }

        [Theory]
        [InlineData(0x00, 0x01, 0x00, 1)]
        [InlineData(0x00, 0x02, 0x00, 2)]
        [InlineData(0x00, 0x00, 0x01, 256)]
        [InlineData(0x01, 0x01, 0x00, 1)]
        [InlineData(0x12, 0x01, 0x01, 257)]
        [InlineData(0xf8, 0x01, 0x00, 0)] //no messageNum on 0xf8 (the 0x01, 0x00 would presumably be part of the CRC and would in any case not be read here)
        public void TestReadMessageNumFromPacketBytes(byte firstByte, byte secondByte, byte thirdByte, ushort expectedValue) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            ushort actualValue = UpdateMessageAck.Read(packetBytes).MessageNum();
            Assert.True(expectedValue == actualValue);
        }

        [Theory]
        [InlineData(0x00, 0x01, 0x00, 0, 1)]
        [InlineData(0x00, 0x02, 0x00, 0, 2)]
        [InlineData(0x00, 0x00, 0x01, 0, 256)]
        [InlineData(0x01, 0x01, 0x00, 1, 1)]
        [InlineData(0x12, 0x01, 0x01, 0x12, 257)]
        public void TestInitializeFromChannelAndMessageNum(byte firstByte, byte secondByte, byte thirdByte, byte channel, ushort messageNumber) {
            UpdateMessageAck updateMessageAck = UpdateMessageAck.Of(channel, messageNumber);
            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            PacketBytes actual = updateMessageAck.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
