using Xunit;
using System;
using System.Collections.Generic;
using Packet;
using Packet.Support;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Support {
    public class BundleTypeAndLengthTest {
        public BundleTypeAndLengthTest() {}

        [Theory]
        [InlineData(0xcf, 0xe0, 0x21, 79)] //Matt's PCAP 155
        [InlineData(0x95, 0x60, 0x5a, 21)] //Matt's PCAP 156
        [InlineData(0x87, 0xe0, 0x01, 7)] //Matt's PCAP 157
        [InlineData(0x9c, 0x62, 0x5a, 284)] //Matt's PCAP 158
        public void TestReadBundleLengthFromPacketBytes(byte firstByte, byte secondByte, byte thirdByte, ushort expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            BundleTypeAndLength bundleTypeAndLength = BundleTypeAndLength.Read(packetBytes);
            ushort actual = bundleTypeAndLength.BundleLength();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0xcf, 0xe0, 0x21, true)] //Matt's PCAP 155
        [InlineData(0x95, 0x60, 0x5a, false)] //Matt's PCAP 156
        [InlineData(0x87, 0xe0, 0x01, true)] //Matt's PCAP 157
        [InlineData(0x9c, 0x62, 0x5a, false)] //Matt's PCAP 158
        public void TestIsSessionMasterFromPacketBytes(byte firstByte, byte secondByte, byte thirdByte, bool expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            bool actual = BundleTypeAndLength.Read(packetBytes).IsSessionMaster();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0xcf, 0xe0, 0x21, false)] //Matt's PCAP 155
        [InlineData(0x95, 0x60, 0x5a, false)] //Matt's PCAP 156
        [InlineData(0x87, 0xe0, 0x01, false)] //Matt's PCAP 157
        [InlineData(0x9c, 0x62, 0x5a, false)] //Matt's PCAP 158
        public void TestServerIsMasterFromPacketBytes(byte firstByte, byte secondByte, byte thirdByte, bool expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            bool actual = BundleTypeAndLength.Read(packetBytes).ServerIsMaster();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0xf4, 0x73, true, true, 500)]
        [InlineData(0xe8, 0x37, true, false, 1000)]
        [InlineData(0xd0, 0x6f, false, true, 2000)]
        [InlineData(0xff, 0x2f, false, false, 2047)]
        public void TestInitializeFromBundleTypeAndLength(byte firstByte, byte secondByte, bool serverIsMaster, bool isHighPhase, ushort bundleLength) {
            BundleTypeAndLength val = BundleTypeAndLength.Of(
                serverIsMaster: serverIsMaster,
                isHighPhase: isHighPhase,
                bundleLength: bundleLength,
                sessionAction: 0);

            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte});
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x87, 0xf0, 0x01, true, true, 7)]
        [InlineData(0xff, 0xb0, 0x01, true, false, 127)]
        [InlineData(0x80, 0xe1, 0x01, false, true, 128)]
        [InlineData(0x81, 0xa1, 0x01, false, false, 129)]
        public void TestInitializeFromBundleTypeAndLengthThreeByteHeader(byte firstByte, byte secondByte, byte thirdByte, bool serverIsMaster, bool isHighPhase, ushort bundleLength) {
            BundleTypeAndLength val = BundleTypeAndLength.Of(
                serverIsMaster: serverIsMaster,
                isHighPhase: isHighPhase,
                bundleLength: bundleLength,
                sessionAction: 0x01);

            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            PacketBytes actual = val.Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, true, 2048)]
        [InlineData(true, true, 3000)]
        [InlineData(true, false, 15000)]
        [InlineData(true, false, 2500)]
        [InlineData(false, true, 2048)]
        [InlineData(false, true, 2050)]
        [InlineData(false, false, 65535)]
        [InlineData(false, false, 2505)]
        public void TestExceptionThrownWhenBundleLengthGreaterThan2047(bool serverIsMaster, bool isHighPhase, ushort bundleLength) {
            Assert.Throws<ArgumentException>(() => BundleTypeAndLength.Of(
                serverIsMaster: serverIsMaster,
                isHighPhase: isHighPhase,
                bundleLength: bundleLength,
                sessionAction: 0));
        }

        [Theory]
        [InlineData(0x95, 0x60)] //Matt's PCAP 156
        [InlineData(0x9c, 0x62)] //Matt's PCAP 158
        public void TestThatOutputIsSameAsInputBytesTwoByteResult(byte firstByte, byte secondByte){
            List<byte> inputBytes = new List<byte>{firstByte, secondByte};
            PacketBytes expected = PacketBytes.Of(inputBytes);
            PacketBytes actual = BundleTypeAndLength.Read(PacketBytes.Of(inputBytes)).Serialize();
            Assert.Equal(expected, actual);
        }

                [Theory]
        [InlineData(0xcf, 0xe0, 0x01)] //Matt's PCAP 155
        [InlineData(0x87, 0xe0, 0x01)] //Matt's PCAP 157
        public void TestThatOutputIsSameAsInputBytesThreeByteResult(byte firstByte, byte secondByte, byte thirdByte){
            List<byte> inputBytes = new List<byte>{firstByte, secondByte, thirdByte};
            PacketBytes expected = PacketBytes.Of(inputBytes);
            PacketBytes actual = BundleTypeAndLength.Read(PacketBytes.Of(inputBytes)).Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
