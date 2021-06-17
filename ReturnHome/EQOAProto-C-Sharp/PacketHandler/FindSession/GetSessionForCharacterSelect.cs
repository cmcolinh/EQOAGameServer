using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace ReturnHome.PacketHandler.FindSession {
    public class GetSessionForCharacterSelect : FindSessionForClientEndpoint {
        private readonly ushort serverEndpoint;
        private readonly Func<ushort, ushort, IPEndPoint, UdpClient, ManageSession> getNewHandleMessage;
        private readonly ConcurrentDictionary<ushort, ManageSession> sessionManagerFor;
        public GetSessionForCharacterSelect Of(ushort serverEndpoint) {
            return new GetSessionForCharacterSelect(serverEndpoint);
        }
        private GetSessionForCharacterSelect(ushort serverEndpoint) {
            this.serverEndpoint = serverEndpoint;
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
            } else {
                //if nothing registered send a new handler (that should initially only accept login packets)
                manageSession = getNewHandleMessage(clientEndpoint, serverEndpoint, ipEndPoint, udpClient);
            }
            return manageSession;
        }
        public FindSessionForClientEndpoint AddHandler(ushort clientEndpoint, ManageSession manageSession) {
            sessionManagerFor[clientEndpoint] = manageSession;
            return this;
        }
        public FindSessionForClientEndpoint RemoveHandler(ushort clientEndpoint) {
            sessionManagerFor.TryRemove(clientEndpoint, out _);
            return this;
        }
    }
}
