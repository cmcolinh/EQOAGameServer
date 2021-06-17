using System;
using System.Net;
using System.Net.Sockets;

namespace ReturnHome.PacketHandler {
    public interface FindSessionForClientEndpoint {
        public static readonly Lazy<FindSessionForClientEndpoint> NullHandler = new Lazy<FindSessionForClientEndpoint>(() => new FindSessionForClientEndpoint.HandleNull());
        ManageSession AndClientEndpoint(ushort clientEndpoint, IPEndPoint ipEndPoint, UdpClient udpClient);
        FindSessionForClientEndpoint AddHandler(ushort clientEndpoint, ManageSession manageSession);
        FindSessionForClientEndpoint RemoveHandler(ushort clientEndpoint);

        private class HandleNull : FindSessionForClientEndpoint {
            public ManageSession AndClientEndpoint(ushort clientEndpoint, IPEndPoint ipEndPoint, UdpClient udpClient) => ManageSession.NullHandler.Value;
            public FindSessionForClientEndpoint AddHandler(ushort clientEndpoint, ManageSession manageSession) => this;
            public FindSessionForClientEndpoint RemoveHandler(ushort clientEndpoint) => this;
        }
    }
}
