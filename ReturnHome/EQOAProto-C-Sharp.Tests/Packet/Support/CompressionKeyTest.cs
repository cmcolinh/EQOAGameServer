using Xunit;
using System.Collections.Generic;
using ReturnHome.Packet;
using ReturnHome.Packet.Support;

namespace ReturnHome.Tests.Packet.Support  {
    public class CompressionKeyTest {
        [Theory] //when first bit of first byte is not set, the second nibble should indicate how many bytes of 0x00s to compress
        [InlineData(0x41, 1)]
        [InlineData(0x32, 2)]
        [InlineData(0x23, 3)]
        public void TestBuildingOneByteCompressionKeysSkippedZeros(byte k, byte expectedZerosToSkip) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{k});
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            byte actualZerosToSkip = compressionKey.ZeroBytesToCompress();
            Assert.Equal(expectedZerosToSkip, actualZerosToSkip);
        }

        [Theory] //when first bit of first byte is not set, The first nibble of the first byte should indicate how many bytes of nonzero data to take after compressing the 0x00s
        [InlineData(0x41, 4)]
        [InlineData(0x32, 3)]
        [InlineData(0x23, 2)]
        public void TestBuildingOneByteCompressionKeysTakenBytes(byte k, byte expectedBytesToTake) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{k});
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            byte actualBytesToTake = compressionKey.NonZeroBytesToTake();
            Assert.Equal(expectedBytesToTake, actualBytesToTake);
        }

        [Theory]
        [InlineData(0x41, 4, 1)]
        [InlineData(0x32, 3, 2)]
        [InlineData(0x23, 2, 3)]
        public void TestBuildingOneByteCompressionKeysFromZerosAndBytes(byte k, byte nonZeroBytesToTake, byte zeroBytesToCompress) {
            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{k});
            CompressionKey compressionKey = CompressionKey.Of(zeroBytesToCompress: zeroBytesToCompress, nonZeroBytesToTake: nonZeroBytesToTake);
            PacketBytes actualBytes = compressionKey.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }

        [Theory] //when first bit of first byte is set, the second byte should indicate how many bytes of 0x00s to compress
        [InlineData(0x80, 0xb9, 185)]
        [InlineData(0x80, 0xba, 186)]
        [InlineData(0x81, 0x19, 25)]
        [InlineData(0x96, 0x01, 1)]
        public void TestBuildingTwoByteCompressionKeysSkippedZeros(byte k1, byte k2, byte expectedZerosToSkip) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{k1, k2});
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            byte actualZerosToSkip = compressionKey.ZeroBytesToCompress();
            Assert.Equal(expectedZerosToSkip, actualZerosToSkip);
        }

        [Theory] //when first bit of first byte is set, the rest of the first byte should indicate how many bytes of nonzero data to take after compressing the 0x00s
        [InlineData(0x80, 0xb9, 0)]
        [InlineData(0x81, 0x19, 1)]
        [InlineData(0x96, 0x01, 22)]
        public void TestBuildingTwoByteCompressionKeysKeptBytes(byte k1, byte k2, byte expectedBytesToTake) {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{k1, k2});
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            byte actualBytesToTake = compressionKey.NonZeroBytesToTake();
            Assert.Equal(expectedBytesToTake, actualBytesToTake);
        }

        [Theory]
        [InlineData(0x80, 0xb9, 0, 185)]
        [InlineData(0x80, 0xba, 0, 186)]
        [InlineData(0x81, 0x19, 1, 25)]
        [InlineData(0x96, 0x01, 22, 1)]
        public void TestBuildingTwoByteCompressionKeysFromZerosAndBytes(byte k1, byte k2, byte nonZeroBytesToTake, byte zeroBytesToCompress) {
            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{k1, k2});
            CompressionKey compressionKey = CompressionKey.Of(zeroBytesToCompress: zeroBytesToCompress, nonZeroBytesToTake: nonZeroBytesToTake);
            PacketBytes actualBytes = compressionKey.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void TestBuildingSentinelHeader() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{0x00});
            CompressionKey compressionKey = CompressionKey.Read(packetBytes);
            Assert.True(compressionKey.IsSentinel());
        }
    }
}