using EQOAProto_C_Sharp.Repository;
using System;
using System.Collections.Generic;
using System.Net;

namespace EQOAProto_C_Sharp.UnitTests.Packet {
    class MockGameServerListRepository : GameServerListRepository {
        private static readonly Lazy<List<GameServerListRepository.GameServer>> gameServers = new Lazy<List<GameServerListRepository.GameServer>>(() => {
            return new List<GameServerListRepository.GameServer>(){
                new CastleLightWolf(),
                new DirenHold(),
                new FerransHope(),
                new Hodstock(),
                new MarrsFist(),
                new ProudpineOutpost(),
                new Hagley()
            };
        });
        public List<GameServerListRepository.GameServer> ServerListFor(string userName, string uuid) {
            return gameServers.Value;
        }
    }

    class CastleLightWolf : GameServerListRepository.GameServer {
        public string ServerName() => "Castle Lightwolf";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0x1f0a;
        public ushort ServerPort() => 10071;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 10, 48});
        public byte ServerLanguage() => 0x00;
    }

    class DirenHold : GameServerListRepository.GameServer {
        public string ServerName() => "Diren Hold";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0x3e01;
        public ushort ServerPort() => 10070;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 10, 72});
        public byte ServerLanguage() => 0x00;
    }
    class FerransHope : GameServerListRepository.GameServer {
        public string ServerName() => "Ferran's Hope";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0x241c;
        public ushort ServerPort() => 10070;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 10, 116});
        public byte ServerLanguage() => 0x00;
    }

    class Hodstock : GameServerListRepository.GameServer {
        public string ServerName() => "Hodstock";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0xb991;
        public ushort ServerPort() => 10070;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 10, 132});
        public byte ServerLanguage() => 0x00;
    }

    class MarrsFist : GameServerListRepository.GameServer {
        public string ServerName() => "Marr's Fist";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0xbd9e;
        public ushort ServerPort() => 10071;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 200, 37});
        public byte ServerLanguage() => 0x00;
    }

    class ProudpineOutpost : GameServerListRepository.GameServer {
        public string ServerName() => "Proudpine Outpost";
        public byte ServerFlag() => 0x00;
        public ushort ServerEndpoint() => 0xd7a9;
        public ushort ServerPort() => 10070;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 200, 68});
        public byte ServerLanguage() => 0x00;
    }

    class Hagley : GameServerListRepository.GameServer {
        public string ServerName() => "Hagley (Test)";
        public byte ServerFlag() => 0x01;
        public ushort ServerEndpoint() => 0x9b35;
        public ushort ServerPort() => 10070;
        public IPAddress ServerIpAddress() => new IPAddress(new byte[]{199, 108, 10, 235});
        public byte ServerLanguage() => 0x00;
    }
}