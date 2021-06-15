using System;
using System.Collections.Generic;

namespace ReturnHome.Packet {
    public interface PacketBundles : BinaryRecord {
        IList<PacketBundle> Bundles();

        public static PacketBundles Read(PacketBytes packetBytes) {
            List<PacketBundle> bundles = new List<PacketBundle>();
            while(packetBytes.Count > 0) {
                bundles.Add(PacketBundle.Read(packetBytes));
            }
            return new PacketBundles.Impl(bundles);
        }

        public static PacketBundles Empty() {
            return new PacketBundles.Impl(new List<PacketBundle>());
        }

        public static PacketBundles Of(PacketBundle packetBundle) {
            List<PacketBundle> bundles = new List<PacketBundle>();
            bundles.Add(packetBundle);
            return new PacketBundles.Impl(bundles: bundles);
        }

        private class Impl : PacketBundles {
            readonly IList<PacketBundle> bundles;
            readonly Lazy<PacketBytes> bytes;

            public IList<PacketBundle> Bundles() => bundles;
            public PacketBytes Serialize() => bytes.Value;

            public Impl(List<PacketBundle> bundles) {
                this.bundles = bundles.AsReadOnly();
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                PacketBytes result = PacketBytes.Of(new List<byte>());
                foreach (PacketBundle bundle in this.bundles) {result = result.Append(bundle.Serialize());}
                return result;
            }
        }
    }
}
