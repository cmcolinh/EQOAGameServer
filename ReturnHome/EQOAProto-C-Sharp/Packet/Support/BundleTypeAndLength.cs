using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Support {
    public interface BundleTypeAndLength : BinaryRecord {
        bool IsSessionMaster();
        bool IsHighPhase();
        bool ServerIsMaster();
        bool ShortSessionId();
        byte SessionAction();
        ushort BundleLength();

        public static BundleTypeAndLength Read(PacketBytes packetBytes) {
            PacketBytes bytes = packetBytes.PopFirst(bytes: 2);
            //the "session master" flag is the first bit of the second byte.  If session master, a session action byte eill be included
            bool sessionActionIncluded = (bytes[1] & 0x80) == 0x80;
            //the "high phase" flag (purpose presently [8/2020] unknown) is the second bit of the second byte
            bool isHighPhase = (bytes[1] & 0x40) == 0x40;
            //the purpose of the third bit of the second byte is unknown.  Is always set AFAIK
            bool shortSessionId = (bytes[1] & 0x20) == 0x20;
            //the fourth bit of the second indicates server is session master when set, and client is master when clear
            bool serverIsMaster = (bytes[1] & 0x10) == 0x10;
            //the purpose of the first bit of the first byte is s.  Is always set AFAIK
            //11 bit value, represented by(last 4 bits of 2nd byte * 128) + (last 7 bits of 1st byte) 
            ushort bundleLength = (ushort)(((bytes[1] & 0x0F) << 7) + (bytes[0] & 0x7F));
            byte sessionAction = 0;
            if (sessionActionIncluded) {
                sessionAction = packetBytes.PopFirst(bytes: 1)[0];
            }

            return new BundleTypeAndLength.Impl(
                isHighPhase: isHighPhase,
                shortSessionId: shortSessionId,
                serverIsMaster: serverIsMaster,
                bundleLength: bundleLength,
                sessionAction: sessionAction);
        }

        public static BundleTypeAndLength Of(bool isHighPhase, bool serverIsMaster, ushort bundleLength, byte sessionAction, bool shortSessionId = true) {
            return new BundleTypeAndLength.Impl(
                isHighPhase: isHighPhase,
                shortSessionId: shortSessionId,
                serverIsMaster: serverIsMaster,
                sessionAction: sessionAction,
                bundleLength: bundleLength);
        }

        private class Impl : BundleTypeAndLength {
            readonly bool isHighPhase;
            readonly bool serverIsMaster;
            readonly bool shortSessionId;
            readonly byte sessionAction;
            readonly ushort bundleLength;
            readonly Lazy<PacketBytes> bytes;

            public bool IsSessionMaster() => sessionAction > 0;
            public bool IsHighPhase() => isHighPhase;
            public bool ServerIsMaster() => serverIsMaster;
            public bool ShortSessionId() => shortSessionId;
            public ushort BundleLength() => bundleLength;
            public byte SessionAction() => sessionAction;
            public PacketBytes Serialize() => bytes.Value;

            public Impl(bool isHighPhase, bool shortSessionId, bool serverIsMaster, ushort bundleLength, byte sessionAction) {
                if (bundleLength > 2047) {
                    throw new ArgumentException($"Bundle Length {bundleLength} is not allowed.  It cannot be greater than 2047 bytes, as it is represented internally by an 11 bit unsigned integer.");
                }
                this.isHighPhase = isHighPhase;
                this.shortSessionId = shortSessionId;
                this.serverIsMaster = serverIsMaster;
                this.bundleLength = bundleLength;
                this.sessionAction = sessionAction;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                if (sessionAction == 0) {
                    return PacketBytes.Of(new List<byte>{
                        (byte)((0x80 + (byte)(bundleLength & 0x007F))),
                        (byte)(Flags() + (bundleLength >> 7))  //bundleLength is 11 bits, so shifting right 7 bits leaves 4 bits
                    });
                }
                return PacketBytes.Of(new List<byte>{
                    (byte)((0x80 + (byte)(bundleLength & 0x007F))),
                    (byte)(Flags() + (bundleLength >> 7)),  //bundleLength is 11 bits, so shifting right 7 bits leaves 4 bits
                    sessionAction
                });
            }

            private byte Flags() => (byte)((sessionAction > 0 ? 0x80 : 0x00) + (isHighPhase ? 0x40 : 0x00) + (shortSessionId ? 0x20 : 0x00) + (serverIsMaster ? 0x10 : 0x00));
        }
    }
}
