using ReturnHome.Packet.Bundle;

namespace ReturnHome.PacketHandler {
    public class NullHandler : HandleMessage {
        public NullHandler() {
        }

        public void Process(BundleMessage bundleMessage) {
        }
    }
}
