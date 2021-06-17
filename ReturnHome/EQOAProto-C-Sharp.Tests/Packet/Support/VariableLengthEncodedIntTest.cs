using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support {
    public class VariableLengthEncodedIntTest {
        public VariableLengthEncodedIntTest() {}

        [Theory] //for positive numbers less than 64, result should simply be input * 2 (two because the last bit is actually the sign flag, so only the middle six bits are used)
        [InlineData(0x00, 0)]
        [InlineData(0x02, 1)]
        [InlineData(0x0e, 7)] //From Matt's PCAP 158
        [InlineData(0x7e, 63)]
        public void TestReadPositiveValuesLessThan64FromSingleByte(byte packetByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{packetByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers not less than -64, result should simply be (((input + 1) * -2) -1)
        [InlineData(0x01, -1)] //note how the 0x01 sign bit is set on all of these
        [InlineData(0x03, -2)]
        [InlineData(0x0f, -8)] //From Matt's PCAP 158
        [InlineData(0x7f, -64)]
        public void TestReadNegativeValuesNotLessThanNegative64FromSingleByte(byte packetByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{packetByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for positive numbers between 64 and 8191, result should be two bytes. Second byte should be (value / 64), first byte is (((value % 64) * 2) + 128) 
        [InlineData(0x80, 0x01, 64)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 stands for 64
        [InlineData(0x82, 0x01, 65)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 in the second byte stands for 64, the 0x02 in the first byte means add 1. (0x01 bit is the sign bit)
        [InlineData(0xfe, 0x01, 127)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 in the second byte stands for 64, the 0x7e in the first byte means add 63.
        [InlineData(0x80, 0x02, 128)] //now a 0x02 in the second byte to represent two 64s, or 128.
        [InlineData(0xfe, 0x7f, 8191)] //taken now to it's two byte maximum value, 0x7f for 127 * 64 = 8128 + 63 = 8191
        public void TestReadPositiveValuesBetween64And8191FromTwoBytes(byte firstByte, byte secondByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers between -65 and 8192, result should be two bytes.
        [InlineData(0x81, 0x01, -65)]
        [InlineData(0x83, 0x01, -66)]
        [InlineData(0xff, 0x01, -128)]
        [InlineData(0x81, 0x02, -129)]
        [InlineData(0xff, 0x7f, -8192)]
        public void TestReadNegativeValuesBetweenNegative65AndNegative8192FromTwoBytes(byte firstByte, byte secondByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for positive numbers between 8192 and 1048575, result should be three bytes. Third byte should be (value / 8192), second byte is ((value % 8192) / 64), first byte is (((value % 64) * 2) + 128) 
        [InlineData(0x80, 0x80, 0x01, 8192)] //the top byte first bit 0x80 simply means read a second byte.  The second byte first bit 0x80 also means read another byte.  the 0x1 stands for 8192
        [InlineData(0x82, 0x80, 0x01, 8193)] //8192 + 1
        [InlineData(0xfe, 0x80, 0x01, 8255)] //8192 + 63
        [InlineData(0x80, 0x81, 0x01, 8256)] //8192 + 64
        [InlineData(0x82, 0x81, 0x01, 8257)] //8192 + 64 + 1
        [InlineData(0x80, 0x80, 0x02, 16384)] //8192 * 2
        [InlineData(0xfe, 0xff, 0x7f, 1048575)] //taken now to it's three byte maximum value, 0x7f for 127 * 1040384 = 8128 + (127 * 64) = 1048512 + 63 = 1048575
        public void TestReadPositiveValuesBetween8192And1048575FromThreeBytes(byte firstByte, byte secondByte, byte thirdByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers between -8193 and -1048576, result should be three bytes.
        [InlineData(0x81, 0x80, 0x01, -8193)]
        [InlineData(0x83, 0x80, 0x01, -8194)]
        [InlineData(0xff, 0x80, 0x01, -8256)]
        [InlineData(0x81, 0x81, 0x01, -8257)]
        [InlineData(0x83, 0x81, 0x01, -8258)]
        [InlineData(0x81, 0x80, 0x02, -16385)]
        [InlineData(0xff, 0xff, 0x7f, -1048576)]
        public void TestReadNegativeValuesBetweenNegative8193AndNegative1048576FromThreeBytes(byte firstByte, byte secondByte, byte thirdByte, long expected) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            long actual = VariableLengthEncodedInt.Read(packetBytes).ToLong();
            Assert.Equal(expected, actual);
        }

        [Theory] //for positive numbers less than 64, result should simply be input * 2 (two because the last bit is actually the sign flag, so only the middle six bits are used)
        [InlineData(0x00, 0)]
        [InlineData(0x02, 1)]
        [InlineData(0x0e, 7)] //From Matt's PCAP 158
        [InlineData(0x7e, 63)]
        public void TestSerializePositiveValuesLessThan64ToSingleByte(byte packetByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{packetByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers not less than -64, result should simply be (((input + 1) * -2) -1)
        [InlineData(0x01, -1)] //note how the 0x01 sign bit is set on all of these
        [InlineData(0x03, -2)]
        [InlineData(0x0f, -8)] //From Matt's PCAP 158
        [InlineData(0x7f, -64)]
        public void TestSerializeNegativeValuesNotLessThanNegative64ToSingleByte(byte packetByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{packetByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory] //for positive numbers between 64 and 8191, result should be two bytes. Second byte should be (value / 64), first byte is (((value % 64) * 2) + 128) 
        [InlineData(0x80, 0x01, 64)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 stands for 64
        [InlineData(0x82, 0x01, 65)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 in the second byte stands for 64, the 0x02 in the first byte means add 1. (0x01 bit is the sign bit)
        [InlineData(0xfe, 0x01, 127)] //the top byte first bit 0x80 simply means read a second byte.  The 0x01 in the second byte stands for 64, the 0x7e in the first byte means add 63.
        [InlineData(0x80, 0x02, 128)] //now a 0x02 in the second byte to represent two 64s, or 128.
        [InlineData(0xfe, 0x7f, 8191)] //taken now to it's two byte maximum value, 0x7f for 127 * 64 = 8128 + 63 = 8191
        public void TestSerializePositiveValuesBetween64And8191ToTwoBytes(byte firstByte, byte secondByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers between -65 and 8192, result should be two bytes.
        [InlineData(0x81, 0x01, -65)]
        [InlineData(0x83, 0x01, -66)]
        [InlineData(0xff, 0x01, -128)]
        [InlineData(0x81, 0x02, -129)]
        [InlineData(0xff, 0x7f, -8192)]
        public void TestSerializeNegativeValuesBetweenNegative65AndNegative8192ToTwoBytes(byte firstByte, byte secondByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory] //for positive numbers between 8192 and 1048575, result should be three bytes. Third byte should be (value / 8192), second byte is ((value % 8192) / 64), first byte is (((value % 64) * 2) + 128) 
        [InlineData(0x80, 0x80, 0x01, 8192)] //the top byte first bit 0x80 simply means read a second byte.  The second byte first bit 0x80 also means read another byte.  the 0x0 stands for 8192
        [InlineData(0x82, 0x80, 0x01, 8193)] //8192 + 1
        [InlineData(0xfe, 0x80, 0x01, 8255)] //8192 + 63
        [InlineData(0x80, 0x81, 0x01, 8256)] //8192 + 64
        [InlineData(0x82, 0x81, 0x01, 8257)] //8192 + 64 + 1
        [InlineData(0x80, 0x80, 0x02, 16384)] //8192 * 2
        [InlineData(0xfe, 0xff, 0x7f, 1048575)] //taken now to it's three byte maximum value, 0x7f for 127 * 1040384 = 8128 + (127 * 64) = 1048512 + 63 = 1048575
        public void TestSerializePositiveValuesBetween8192And1048575ToThreeBytes(byte firstByte, byte secondByte, byte thirdByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }

        [Theory] //for negative numbers between -8193 and -1048576, result should be three bytes.
        [InlineData(0x81, 0x80, 0x01, -8193)]
        [InlineData(0x83, 0x80, 0x01, -8194)]
        [InlineData(0xff, 0x80, 0x01, -8256)]
        [InlineData(0x81, 0x81, 0x01, -8257)]
        [InlineData(0x83, 0x81, 0x01, -8258)]
        [InlineData(0x81, 0x80, 0x02, -16385)]
        [InlineData(0xff, 0xff, 0x7f, -1048576)]
        public void TestSerializeNegativeValuesBetweenNegative8193AndNegative1048576ToThreeBytes(byte firstByte, byte secondByte, byte thirdByte, long val) {
            PacketBytes expected = PacketBytes.Of(new List<byte>{firstByte, secondByte, thirdByte});
            PacketBytes actual = VariableLengthEncodedInt.Of(val).Serialize();
            Assert.Equal(expected, actual);
        }
    }
}
