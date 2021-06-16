using Xunit;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class UCS2StringLSTest {
        public UCS2StringLSTest() {}

        [Fact]
        public void TestReadFromPacketBytes() {//                     C           a           s           t           l           e         space         L           i           g           h           t           w           o           l           f
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x43, 0x00, 0x61, 0x00, 0x73, 0x00, 0x74, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x20, 0x00, 0x4c, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00, 0x74, 0x00, 0x77, 0x00, 0x6f, 0x00, 0x6c, 0x00, 0x66, 0x00});
            string actualValue = UCS2StringLe.Read(packetBytes).ToString();
            string expectedValue = "Castle Lightwolf";
            Assert.True(expectedValue == actualValue);
        }

        [Fact]
        public void TestInitializeFromString() {
            UCS2StringLe val = UCS2StringLe.Of("Hodstock"); //     H           o           d           s           t           o           c           k
            PacketBytes expected = PacketBytes.Of(new List<byte>{0x48, 0x00, 0x6f, 0x00, 0x64, 0x00, 0x73, 0x00, 0x74, 0x00, 0x6f, 0x00, 0x63, 0x00, 0x6b, 0x00});
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
