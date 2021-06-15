using System.Collections.Generic;
using System.Net;
using ReturnHome.Packet.Bundle.Message.Types;

namespace ReturnHome.Repository {
    public interface GameServerListRepository {
        /// <summary> get the server, appropriate for the given user and credentials </summary>
        List<GameServerListRepository.GameServer> ServerListFor(string userName, string uuid);

        public interface GameServer {
            string ServerName();
            byte ServerFlag();
            ushort ServerEndpoint();
            ushort ServerPort();
            IPAddress ServerIpAddress();
            byte ServerLanguage();
            GameServerList.GameServer ToBinaryRecord() => GameServerList.GameServer.Of(
                serverName: ServerName(),
                serverFlag: ServerFlag(),
                serverEndpoint: ServerEndpoint(),
                serverPort: ServerPort(),
                serverIpAddress: ServerIpAddress(),
                serverLanguage: ServerLanguage());
        }
    }
}
