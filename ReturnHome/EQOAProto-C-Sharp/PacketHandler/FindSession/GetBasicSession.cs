using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace ReturnHome.PacketHandler.FindSession {
    public class GetBasicSession : FindSessionForClientEndpoint {
        private readonly ConcurrentDictionary<ushort, ManageSession> sessionManagerFor;
        public GetBasicSession Of() {
            return new GetBasicSession();
        }
        private GetBasicSession() {
            this.sessionManagerFor = new ConcurrentDictionary<ushort, ManageSession>();
        }
        public ManageSession AndClientEndpoint(ushort clientEndpoint, IPEndPoint ipEndPoint, UdpClient udpClient) {
            ManageSession manageSession = ManageSession.NullHandler.Value;
            if (sessionManagerFor.ContainsKey(clientEndpoint)) {
                //get the session manager associated with this client endpoint
                manageSession = sessionManagerFor[clientEndpoint];
                //reject using this manager if the IP address doesn't match the registered endpoint
                if (!ipEndPoint.Equals(manageSession.IPEndPoint())) {
                    manageSession = ManageSession.NullHandler.Value;
                }
                manageSession = manageSession.VerifySession();
            }
            return manageSession;
        }
        public FindSessionForClientEndpoint AddHandler(ushort clientEndpoint, ManageSession manageSession) {
            sessionManagerFor.TryAdd(clientEndpoint, manageSession);
            return this;
        }
        public FindSessionForClientEndpoint RemoveHandler(ushort clientEndpoint) {
            sessionManagerFor.TryRemove(clientEndpoint, out _);
            return this;
        }
    }
}
