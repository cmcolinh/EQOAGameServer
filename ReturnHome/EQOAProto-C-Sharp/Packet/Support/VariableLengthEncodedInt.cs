using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface VariableLengthEncodedInt : BinaryRecord {
        long ToLong();

        public static VariableLengthEncodedInt Of(long val) {
            if (val > -1 && val < 64) {
                return new VariableLengthEncodedInt.PositiveSingleByteImpl((byte) val);
            }
            return new VariableLengthEncodedInt.MultiByteImpl(val);
        }

        public static VariableLengthEncodedInt Read(PacketBytes packetBytes) {
            //consume the first byte from the remaining packet bytes and use the value
            byte firstByte = packetBytes.PopFirst(bytes: 1)[0];
            //the sign bit is the last bit of the first byte
            bool isNegative = (firstByte & 0x01) == 0x01;
            //the continuation flag is the first bit of each byte
            bool moreBytesRequired = (firstByte & 0x80) == 0x80;
            //the middle 6 bits of the first byte are the least significant part of the value
            long val = (firstByte & 0x7e) >> 1; // & 0x7e means to only consider the middle 6 bits, >> 1 means shift right one bit
            if (moreBytesRequired) {
                val = GetNextByte(val: val, bitsToShift: 6, packetBytes: packetBytes);
            }
            if (!isNegative && val < 64) {
                return new PositiveSingleByteImpl((byte) val);
            }
            if (isNegative) {val = ~val;} //val is simply the twos complement of the value we read
            return new VariableLengthEncodedInt.MultiByteImpl(val);
        }

        private static long GetNextByte(long val, byte bitsToShift, PacketBytes packetBytes) {
            //consume the first byte from the remaining packet bytes and use the value
            byte nextByte = packetBytes.PopFirst(bytes: 1)[0];
            //the continuation flag is the first bit of each byte
            bool moreBytesRequired = (nextByte & 0x80) == 0x80;
            //the last 7 bits of bytes *after* the first byte are the next part of the value, the bitsToShift gives the exponent
            val += ((nextByte & 0x7f) << bitsToShift); // &0x7f means use only the last 7 bits, << is left shift (same as multiply by 2^x)
            //recursively take more bytes until we have the whole value, incrementing our shift by an additional 7 payload bits on each iteration
            if (moreBytesRequired) {
                val = GetNextByte(val: val, bitsToShift: (byte)(bitsToShift + 7), packetBytes: packetBytes);
            }
            return val;
        }

        private class PositiveSingleByteImpl : VariableLengthEncodedInt {
            readonly byte val;
            readonly Lazy<PacketBytes> bytes;

            public long ToLong() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public PositiveSingleByteImpl(byte val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{(byte)(this.val << 1)}));
            }
        }

        private class MultiByteImpl : VariableLengthEncodedInt {
            readonly long val;
            readonly Lazy<PacketBytes> bytes;

            public long ToLong() => val;
            public override string ToString() => $"{val}";
            public PacketBytes Serialize() => bytes.Value;

            public MultiByteImpl(long val) {
                this.val = val;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                bool isNegative = val < 0;
                long tempVal = val;
                if (isNegative) {tempVal = ~tempVal;} //calculating against the twos complements results in the correct bits
                //start with the least significant 6 digits
                byte firstByte = (byte)((tempVal & 0x3f) << 1);
                if (isNegative) {firstByte |= 0x01;} //add the sign bit to the first byte if necessary
                List<byte> outputBytes = new List<byte>{firstByte};
                tempVal = tempVal >> 6;
                bool moreBytesRequired = tempVal > 0;
                if (moreBytesRequired) {
                    outputBytes[0] |= 0x80; //add the "needs more bytes" flag at the top (0x80) bit
                    outputBytes = AddByte(outputBytes: outputBytes, tempVal: tempVal);
                }
                return PacketBytes.Of(outputBytes);
            }

            private List<byte> AddByte(List<byte> outputBytes, long tempVal) {
                byte nextByte = (byte)(tempVal & 0x7f);
                tempVal = tempVal >> 7;
                bool moreBytesRequired = tempVal > 0;
                if (moreBytesRequired) {
                    nextByte |= 0x80; //add the "needs more bytes" flag at the top (0x80) bit
                    outputBytes.Add(nextByte);
                    outputBytes = AddByte(outputBytes: outputBytes, tempVal: tempVal);
                } else {
                    outputBytes.Add(nextByte);
                }
                return outputBytes;
            }
        }
    }
}
