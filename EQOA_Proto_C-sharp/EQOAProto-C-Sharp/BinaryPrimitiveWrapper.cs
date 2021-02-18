﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQOAProto
{
    class BinaryPrimitiveWrapper
    {
        public static int GetLEInt(ReadOnlyMemory<byte> mem, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadInt32LittleEndian(mem.Span.Slice(offset - 4, 4));
        }

        public static uint GetLEUint(ReadOnlyMemory<byte> mem, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadUInt32LittleEndian(mem.Span.Slice(offset - 4, 4));
        }
        public static short GetLEShort(ReadOnlyMemory<byte> mem, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadInt16LittleEndian(mem.Span.Slice(offset - 2, 2));
        }

        public static ushort GetLEUShort(ReadOnlyMemory<byte> mem, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadUInt16LittleEndian(mem.Span.Slice(offset - 2, 2));
        }

        public static byte GetLEByte(ReadOnlyMemory<byte> mem, ref int offset)
        {
            offset += 1;
            return mem.Span[offset - 1];
        }
    }
}
