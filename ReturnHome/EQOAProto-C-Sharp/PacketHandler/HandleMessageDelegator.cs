using ReturnHome.Packet.Bundle;

namespace ReturnHome.PacketHandler {
    public interface HandleMessageDelegator : HandleMessage {
        HandleMessageDelegator Delegate(HandleMessage to);

        static HandleMessageDelegator NewInstance() {
            return new HandleMessageDelegator.Impl();
        }

        private class Impl : HandleMessageDelegator {
            private HandleMessage delegationTarget;

            public Impl(){
                delegationTarget = NullHandler.Value;
            }

            public void Process(BundleMessage bundleMessage) => delegationTarget.Process(bundleMessage);

            public HandleMessageDelegator Delegate(HandleMessage to) {
                this.delegationTarget = to;
                return this;
            }
        }
    }
}
