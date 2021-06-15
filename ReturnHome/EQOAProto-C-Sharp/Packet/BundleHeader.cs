using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet {
    public interface BundleHeader : BinaryRecord {
        bool IsSessionMaster();
        bool ServerIsMaster();
        bool IsHighPhase();
        ushort BundleLength();
        byte SessionAction();
        uint SessionIdBase();
        uint SessionIdUp();

        public static BundleHeader Read(PacketBytes packetBytes) {
            BundleTypeAndLength bundleTypeAndLength = BundleTypeAndLength.Read(packetBytes);
            bool serverIsMaster = bundleTypeAndLength.ServerIsMaster();
            SessionInfo sessionInfo = SessionInfo.Read(packetBytes, shortSessionId: serverIsMaster);
            return new BundleHeader.Impl(bundleTypeAndLength, sessionInfo);
        }

        public static BundleHeader Of(BundleTypeAndLength bundleTypeAndLength, SessionInfo sessionInfo) {
            return new BundleHeader.Impl(bundleTypeAndLength, sessionInfo);
        }

        private class Impl : BundleHeader {
            readonly BundleTypeAndLength bundleTypeAndLength;
            readonly SessionInfo sessionInfo;
            readonly Lazy<PacketBytes> bytes;

            public bool IsSessionMaster() => bundleTypeAndLength.IsSessionMaster();
            public bool ServerIsMaster() => bundleTypeAndLength.ServerIsMaster();
            public bool IsHighPhase() => bundleTypeAndLength.IsHighPhase();
            public ushort BundleLength() => bundleTypeAndLength.BundleLength();
            public byte SessionAction() => bundleTypeAndLength.SessionAction();
            public uint SessionIdBase() => sessionInfo.SessionIdBase();
            public uint SessionIdUp() => sessionInfo.SessionIdUp();
            public PacketBytes Serialize() => bytes.Value;

            public Impl(BundleTypeAndLength bundleTypeAndLength, SessionInfo sessionInfo) {
                this.bundleTypeAndLength = bundleTypeAndLength;
                this.sessionInfo = sessionInfo;
                this.bytes = new Lazy<PacketBytes>(() => this.bundleTypeAndLength.Serialize()
                    .Append(sessionInfo.Serialize()));
            }
        }
    }
}
