using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet {
    public interface SessionInfo : BinaryRecord {
        uint SessionIdBase();
        uint SessionIdUp();

        private static readonly Dictionary<bool, Func<PacketBytes, SessionInfo>> GetSessionActionFor = new Dictionary<bool, Func<PacketBytes, SessionInfo>> {
            {false, packetBytes => SessionInfo.ClientSessionInfo.Read(packetBytes.PopFirst(bytes: 4))},
            {true, packetBytes => SessionInfo.ServerSessionInfo.Read(packetBytes)},
        };

        private static readonly Dictionary<bool, Func<uint, uint, SessionInfo>> BuildSessionActionFor = new Dictionary<bool, Func<uint, uint, SessionInfo>> {
            {false, (sessionIdBase, sessionIdUp) => SessionInfo.ServerSessionInfo.Of(sessionIdBase, (long)sessionIdUp)},
            {true, (sessionIdBase, sessionIdUp) => SessionInfo.ClientSessionInfo.Of((ushort)sessionIdBase, (ushort)sessionIdUp)}
        };

        public static SessionInfo Read(PacketBytes packetBytes, bool shortSessionId) {
            return GetSessionActionFor[shortSessionId](packetBytes);
        }

        public static SessionInfo Of(bool shortSessionId, uint sessionIdBase, uint sessionIdUp) {
            return BuildSessionActionFor[shortSessionId](sessionIdBase, sessionIdUp);
        }

        private class ClientSessionInfo : SessionInfo {
            readonly Uint16Le sessionIdBase;
            readonly Uint16Le sessionIdUp;
            readonly Lazy<PacketBytes> bytes;

            public uint SessionIdBase() => sessionIdBase.ToUshort();
            public uint SessionIdUp() => sessionIdUp.ToUshort();
            public PacketBytes Serialize() => bytes.Value;

            public static SessionInfo Read(PacketBytes with) {
                PacketBytes packetBytes = with;
                Uint16Le sessionIdBase = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
                Uint16Le sessionIdUp = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
                return new SessionInfo.ClientSessionInfo(sessionIdBase, sessionIdUp);
            }

            public static SessionInfo Of(ushort sessionIdBase, ushort sessionIdUp) {
                return new ClientSessionInfo(
                    sessionIdBase: Uint16Le.Of(sessionIdBase),
                    sessionIdUp: Uint16Le.Of(sessionIdUp)
                );
            }

            public ClientSessionInfo(Uint16Le sessionIdBase, Uint16Le sessionIdUp) {
                this.sessionIdBase = sessionIdBase;
                this.sessionIdUp = sessionIdUp;
                this.bytes = new Lazy<PacketBytes>(() => sessionIdBase.Serialize()
                    .Append(sessionIdUp.Serialize()));
            }
        }

        private class ServerSessionInfo : SessionInfo {
            readonly Uint32Le sessionIdBase;
            readonly VariableLengthEncodedInt sessionIdUp;
            readonly Lazy<PacketBytes> bytes;

            public uint SessionIdBase() => sessionIdBase.ToUint();
            public uint SessionIdUp() => (uint)sessionIdUp.ToLong();
            public PacketBytes Serialize() => bytes.Value;

            public static SessionInfo Read(PacketBytes packetBytes) {
                Uint32Le sessionIdBase = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
                VariableLengthEncodedInt sessionIdUp = VariableLengthEncodedInt.Read(packetBytes);
                return new SessionInfo.ServerSessionInfo(sessionIdBase, sessionIdUp);
            }

            public static SessionInfo Of(uint sessionIdBase, long sessionIdUp) => new SessionInfo.ServerSessionInfo(
                sessionIdBase: Uint32Le.Of(sessionIdBase),
                sessionIdUp: VariableLengthEncodedInt.Of(sessionIdUp));

            public ServerSessionInfo(Uint32Le sessionIdBase, VariableLengthEncodedInt sessionIdUp) {
                this.sessionIdBase = sessionIdBase;
                this.sessionIdUp = sessionIdUp;
                this.bytes = new Lazy<PacketBytes>(() => sessionIdBase.Serialize()
                    .Append(sessionIdUp.Serialize()));
            }
        }
    }
}
