//using System;
//using System.Collections.Generic;

//namespace ReturnHome.Packet.Support {
//    public interface VariableLengthEncodedUnsignedInt : BinaryRecord {
//        ulong ToULong();

//        public static VariableLengthEncodedUnsignedInt Of(ulong val) {
//            if (val < 128) {
//                return new VariableLengthEncodedUnsignedInt.SingleByteImpl((byte) val);
//            }
//            return new VariableLengthEncodedUnsignedInt.MultiByteImpl(val);
//        }

//        public static VariableLengthEncodedUnsignedInt Read(PacketBytes packetBytes) {
            //consume the first byte from the remaining packet bytes and use the value
//            byte firstByte = packetBytes.PopFirst(bytes: 1)[0];
            //the continuation flag is the first bit of each byte
//            bool moreBytesRequired = (firstByte & 0x80) == 0x80;
            //the last 7 bits of the first byte are the least significant part of the value
//            ulong val = ((ulong)(firstByte & 0x7f));// consider the last 7 bits only
//            if (moreBytesRequired) {
//                val = GetNextByte(val: val, bitsToShift: 7, packetBytes: packetBytes);
//            }
//            if (val < 128) {
//                return new VariableLengthEncodedUnsignedInt.SingleByteImpl((byte) val);
//            }
//            return new VariableLengthEncodedUnsignedInt.MultiByteImpl(val);
//        }

//        private static ulong GetNextByte(ulong val, byte bitsToShift, PacketBytes packetBytes) {
            //consume the first byte from the remaining packet bytes and use the value
//            byte nextByte = packetBytes.PopFirst(bytes: 1)[0];
            //the continuation flag is the first bit of each byte
//            bool moreBytesRequired = (nextByte & 0x80) == 0x80;
            //the last 7 bits of bytes *after* the first byte are the next part of the value, the bitsToShift gives the exponent
//            val += (((ulong)(nextByte & 0x7f)) << bitsToShift); // &0x7f means use only the last 7 bits, << is left shift (same as multiply by 2^x)
            //recursively take more bytes until we have the whole value, incrementing our shift by an additional 7 payload bits on each iteration
//            if (moreBytesRequired) {
//                val = GetNextByte(val: val, bitsToShift: (byte)(bitsToShift + 7), packetBytes: packetBytes);
//            }
//            return val;
//        }

//        private class SingleByteImpl : VariableLengthEncodedUnsignedInt {
//            readonly byte val;
//            readonly Lazy<PacketBytes> bytes;

//            public ulong ToULong() => val;
//            public override string ToString() => $"{val}";
//            public PacketBytes Serialize() => bytes.Value;

//            public SingleByteImpl(byte val) {
//                this.val = val;
//                this.bytes = new Lazy<PacketBytes>(() => PacketBytes.Of(new List<byte>{(byte)(this.val << 1)}));
//            }
//        }

//        private class MultiByteImpl : VariableLengthEncodedUnsignedInt {
//            readonly ulong val;
//            readonly Lazy<PacketBytes> bytes;

//            public ulong ToULong() => val;
//            public override string ToString() => $"{val}";
//            public PacketBytes Serialize() => bytes.Value;

//            public MultiByteImpl(ulong val) {
//                this.val = val;
//                this.bytes = new Lazy<PacketBytes>(() => Bytes());
//            }

//            private PacketBytes Bytes() {
//                ulong tempVal = val;
                //start with the least significant 7 digits
//                byte firstByte = (byte)(tempVal & 0x7f);
//                List<byte> outputBytes = new List<byte>{firstByte};
//                tempVal = tempVal >> 7;
//                bool moreBytesRequired = tempVal > 0;
//                if (moreBytesRequired) {
//                    outputBytes[0] |= 0x80; //add the "needs more bytes" flag at the top (0x80) bit
//                    outputBytes = AddByte(outputBytes: outputBytes, tempVal: tempVal);
//                }
//                return PacketBytes.Of(outputBytes);
//            }

//            private List<byte> AddByte(List<byte> outputBytes, long tempVal) {
//                byte nextByte = (byte)(tempVal & 0x7f);
//                tempVal = tempVal >> 7;
//                bool moreBytesRequired = tempVal > 0;
//                if (moreBytesRequired) {
//                    nextByte |= 0x80; //add the "needs more bytes" flag at the top (0x80) bit
//                    outputBytes.Add(nextByte);
//                    outputBytes = AddByte(outputBytes: outputBytes, tempVal: tempVal);
//                } else {
//                    outputBytes.Add(nextByte);
//                }
//                return outputBytes;
//            }
//        }
//    }
//}
