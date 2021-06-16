using ReturnHome.Packet.Bundle.Message.Types;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle {
    public interface SessionClose : BundleContents {

        public static BundleContents Read(PacketBytes packetBytes) {
            CloseSession closeSession = CloseSession.Read(packetBytes.PopFirst(bytes: 4));
            return new SessionClose.Impl(closeSession);
        }

        private class Impl : SessionClose {
            readonly CloseSession closeSession;

            public ushort BundleNumber() => 0;
            public byte BundleType() => 0;
            public ushort BundleAcknowledged() => 0;
            public ushort ReliableMessageAcknowledged() => 0;
            public bool HasAcks() => false;
            public IList<BundleMessage> Messages() => new List<BundleMessage>{closeSession};
            public PacketBytes Serialize() => closeSession.Serialize();

            public Impl(CloseSession closeSession) => this.closeSession = closeSession;
        }
    }
}
