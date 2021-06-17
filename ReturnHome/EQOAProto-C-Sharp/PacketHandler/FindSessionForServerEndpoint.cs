using System.Collections.Generic;

namespace ReturnHome.PacketHandler {
    public interface FindSessionForServerEndpoint {
        FindSessionForClientEndpoint ForServerEndpoint(ushort serverEndpoint);
        FindSessionForServerEndpoint AddHandler(FindSessionForClientEndpoint handlerToAdd, ushort serverEndpoint);
        FindSessionForServerEndpoint RemoveHandler(ushort serverEndpoint);

        public class Impl : FindSessionForServerEndpoint {
            readonly Dictionary<ushort, FindSessionForClientEndpoint> serverEndpointHandlers;
            public Impl() {
                serverEndpointHandlers = new Dictionary<ushort, FindSessionForClientEndpoint>();
            }

            public FindSessionForClientEndpoint ForServerEndpoint(ushort serverEndpoint) {
                return serverEndpointHandlers.GetValueOrDefault(serverEndpoint, FindSessionForClientEndpoint.NullHandler.Value);
            }

            public FindSessionForServerEndpoint AddHandler(FindSessionForClientEndpoint handlerToAdd, ushort serverEndpoint) {
                serverEndpointHandlers.Add(serverEndpoint, handlerToAdd);
                return this;
            }

            public FindSessionForServerEndpoint RemoveHandler(ushort serverEndpoint) {
                serverEndpointHandlers.Remove(serverEndpoint);
                return this;
            }
        }
    }
}
