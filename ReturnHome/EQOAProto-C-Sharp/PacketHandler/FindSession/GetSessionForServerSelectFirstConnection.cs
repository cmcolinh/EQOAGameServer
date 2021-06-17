
using System;
using System.Net;
using System.Net.Sockets;

namespace ReturnHome.PacketHandler.FindSession {
    public class GetSessionForServerSelectFirstConnection : FindSessionForClientEndpoint {
        private readonly Func<ushort, IPEndPoint, UdpClient, ManageSession> getSessionManagerFor;
        public GetSessionForServerSelectFirstConnection Of(Func<ushort, IPEndPoint, UdpClient, ManageSession> getSessionManagerFor) {
            return new GetSessionForServerSelectFirstConnection(getSessionManagerFor: getSessionManagerFor);
        }
        public GetSessionForServerSelectFirstConnection(Func<ushort, IPEndPoint, UdpClient, ManageSession> getSessionManagerFor) {
            this.getSessionManagerFor = getSessionManagerFor;
        }
        public ManageSession AndClientEndpoint(ushort clientEndpoint, IPEndPoint ipEndPoint, UdpClient udpClient) {
            return getSessionManagerFor(clientEndpoint, ipEndPoint, udpClient);
        }
        public FindSessionForClientEndpoint AddHandler(ushort clientEndpoint, ManageSession manageSession) {
            //can't add sessions to this endpoint
            return this;
        }
        public FindSessionForClientEndpoint RemoveHandler(ushort clientEndpoint) {
            //can't remove sessions from this endpoint
            return this;
        }
    }
}
