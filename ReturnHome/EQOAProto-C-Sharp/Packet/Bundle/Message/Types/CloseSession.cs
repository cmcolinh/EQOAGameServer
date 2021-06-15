using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CloseSession : BundleMessage {
        uint SessionId();

        public static new CloseSession Read(PacketBytes packetBytes) {
            Uint32Le sessionId = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new CloseSession.Impl(sessionId);
        }

        private class Impl : CloseSession {
            readonly Uint32Le sessionId;

            void BundleMessage.Accept(HandleMessage handleMessage) => handleMessage.ProcessCloseSession(this);
            ushort BundleMessage.MessageNumber() => 0;
            byte BundleMessage.MessageChannel() => 0xff;
            PacketBytes BinaryRecord.Serialize() => sessionId.Serialize();
            public uint SessionId() => sessionId.ToUint();

            public Impl(Uint32Le sessionId) => this.sessionId = sessionId;
        }
    }
}
