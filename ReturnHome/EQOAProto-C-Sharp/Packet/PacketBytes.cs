using System.Collections.Generic;

namespace ReturnHome.Packet {
    /// <summary> This interface is a thin wrapper around IList<byte> with added convenience methods </summary>
    public interface PacketBytes : IList<byte> {
        /// <summary> Pops the first bytes off of the PacketBytes, removing them from the PacketBytes, and returning the removed PacketBytes </summary>
        PacketBytes PopFirst(int bytes);
        /// <summary> Pops all but but the last bytes off of the PacketBytes, removing them from the PacketBytes, and returning the removed PacketBytes </summary>
        PacketBytes PopAllButLast(int bytes);
        /// <summary> Creates a new PacketBytes object with the argument bytes added to the end of this PacketBytes </summary>
        PacketBytes Append(PacketBytes bytes);

        /// <summary> Wraps the provided List of bytes into a PacketBytes object </summary>
        public static PacketBytes Of(List<byte> bytes) {
            PacketBytes returnBytes = new PacketBytes.Impl();
            bytes.ForEach(i => returnBytes.Add(i));
            return returnBytes;
        }

        private class Impl : List<byte>, PacketBytes {
            public PacketBytes PopFirst(int Bytes) {
                List<byte> result = GetRange(0, Bytes);
                RemoveRange(0, Bytes);
                return PacketBytes.Of(result);
            }

            public PacketBytes PopAllButLast(int Bytes) {
                List<byte> result = GetRange(0, Count - Bytes);
                RemoveRange(0, Count - Bytes);
                return PacketBytes.Of(result);
            }

            public PacketBytes Append(PacketBytes bytesToAppend) {
                PacketBytes bytes = PacketBytes.Of(this);
                foreach (byte b in bytesToAppend){bytes.Add(b);}
                return bytes;
            }
        }
    }
}
