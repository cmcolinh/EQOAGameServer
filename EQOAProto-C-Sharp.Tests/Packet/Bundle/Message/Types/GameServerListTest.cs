using Xunit;
using System.Collections.Generic;
using System.Net;
using EQOAProto_C_Sharp.Repository;
using Packet;
using Packet.Bundle.Message;
using Packet.Bundle.Message.Types;

namespace EQOAProto_C_Sharp.UnitTests.Packet.Bundle.Message.Types {
    public class GameServerListTest {
        [Fact]
        public void TestBuildGameServerList() {
            PacketBytes expectedBytes = ExpectedOpcode().Append(ExpectedBytes());
            GameServerListRepository gameServerListRepository = new MockGameServerListRepository();
            List<GameServerListRepository.GameServer> serverList = gameServerListRepository.ServerListFor(userName: "userName", uuid: "mockUUID");
            GameServerList gameServerList = GameServerList.Of(serverList);
            OpcodeAndMessage opcodeAndMessage = gameServerList.ToOpcodeAndMessage();
            PacketBytes actualBytes = opcodeAndMessage.Serialize();
            Assert.Equal(expectedBytes, actualBytes);
        }

        PacketBytes ExpectedBytes() => PacketBytes.Of(new List<byte> {
            0x0e, 0x10, 0x00, 0x00, 0x00, 0x43, 0x00, 0x61, 0x00, 0x73, 0x00, 0x74, 0x00, 0x6c, 0x00, 0x65,
            0x00, 0x20, 0x00, 0x4c, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00, 0x74, 0x00, 0x77, 0x00, 0x6f,
            0x00, 0x6c, 0x00, 0x66, 0x00, 0x00, 0x0a, 0x1f, 0x57, 0x27, 0x30, 0x0a, 0x6c, 0xc7, 0x00, 0x0a,
            0x00, 0x00, 0x00, 0x44, 0x00, 0x69, 0x00, 0x72, 0x00, 0x65, 0x00, 0x6e, 0x00, 0x20, 0x00, 0x48,
            0x00, 0x6f, 0x00, 0x6c, 0x00, 0x64, 0x00, 0x00, 0x01, 0x3e, 0x56, 0x27, 0x48, 0x0a, 0x6c, 0xc7,
            0x00, 0x0d, 0x00, 0x00, 0x00, 0x46, 0x00, 0x65, 0x00, 0x72, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6e,
            0x00, 0x27, 0x00, 0x73, 0x00, 0x20, 0x00, 0x48, 0x00, 0x6f, 0x00, 0x70, 0x00, 0x65, 0x00, 0x00,
            0x1c, 0x24, 0x56, 0x27, 0x74, 0x0a, 0x6c, 0xc7, 0x00, 0x08, 0x00, 0x00, 0x00, 0x48, 0x00, 0x6f,
            0x00, 0x64, 0x00, 0x73, 0x00, 0x74, 0x00, 0x6f, 0x00, 0x63, 0x00, 0x6b, 0x00, 0x00, 0x91, 0xb9,
            0x56, 0x27, 0x84, 0x0a, 0x6c, 0xc7, 0x00, 0x0b, 0x00, 0x00, 0x00, 0x4d, 0x00, 0x61, 0x00, 0x72,
            0x00, 0x72, 0x00, 0x27, 0x00, 0x73, 0x00, 0x20, 0x00, 0x46, 0x00, 0x69, 0x00, 0x73, 0x00, 0x74,
            0x00, 0x00, 0x9e, 0xbd, 0x57, 0x27, 0x25, 0xc8, 0x6c, 0xc7, 0x00, 0x11, 0x00, 0x00, 0x00, 0x50,
            0x00, 0x72, 0x00, 0x6f, 0x00, 0x75, 0x00, 0x64, 0x00, 0x70, 0x00, 0x69, 0x00, 0x6e, 0x00, 0x65,
            0x00, 0x20, 0x00, 0x4f, 0x00, 0x75, 0x00, 0x74, 0x00, 0x70, 0x00, 0x6f, 0x00, 0x73, 0x00, 0x74,
            0x00, 0x00, 0xa9, 0xd7, 0x56, 0x27, 0x44, 0xc8, 0x6c, 0xc7, 0x00, 0x0d, 0x00, 0x00, 0x00, 0x48,
            0x00, 0x61, 0x00, 0x67, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x79, 0x00, 0x20, 0x00, 0x28, 0x00, 0x54,
            0x00, 0x65, 0x00, 0x73, 0x00, 0x74, 0x00, 0x29, 0x00, 0x01, 0x35, 0x9b, 0x56, 0x27, 0xeb, 0x0a,
            0x6c, 0xc7, 0x00
        });
        PacketBytes ExpectedOpcode() => PacketBytes.Of(new List<byte> {0xb3, 0x07});
    }

    class MockGameServerListRepository : GameServerListRepository{
        List<GameServerListRepository.GameServer> GameServerListRepository.ServerListFor(string userName, string uuid) =>  new List<GameServerListRepository.GameServer>{
            new MockGameServerEntry(serverName: "Castle Lightwolf", serverFlag: 0x00, serverEndpoint: 0x1f0a, serverPort: 10071, serverIpAddress: IPAddress.Parse("199.108.10.48"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Diren Hold", serverFlag: 0x00, serverEndpoint: 0x3e01, serverPort: 10070, serverIpAddress: IPAddress.Parse("199.108.10.72"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Ferran's Hope", serverFlag: 0x00, serverEndpoint: 0x241c, serverPort: 10070, serverIpAddress: IPAddress.Parse("199.108.10.116"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Hodstock", serverFlag: 0x00, serverEndpoint: 0xb991, serverPort: 10070, serverIpAddress: IPAddress.Parse("199.108.10.132"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Marr's Fist", serverFlag: 0x00, serverEndpoint: 0xbd9e, serverPort: 10071, serverIpAddress: IPAddress.Parse("199.108.200.37"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Proudpine Outpost", serverFlag: 0x00, serverEndpoint: 0xd7a9, serverPort: 10070, serverIpAddress: IPAddress.Parse("199.108.200.68"), serverLanguage: 0x00),
            new MockGameServerEntry(serverName: "Hagley (Test)", serverFlag: 0x01, serverEndpoint: 0x9b35, serverPort: 10070, serverIpAddress: IPAddress.Parse("199.108.10.235"), serverLanguage: 0x00)
        };

        public MockGameServerListRepository(){}
    }

    class MockGameServerEntry : GameServerListRepository.GameServer {
        readonly string serverName;
        readonly byte serverFlag;
        readonly ushort serverEndpoint;
        readonly ushort serverPort;
        readonly IPAddress serverIpAddress;
        readonly byte serverLanguage;

        public MockGameServerEntry(string serverName, byte serverFlag, ushort serverEndpoint, ushort serverPort, IPAddress serverIpAddress, byte serverLanguage){
            this.serverName = serverName;
            this.serverFlag = serverFlag;
            this.serverEndpoint = serverEndpoint;
            this.serverPort = serverPort;
            this.serverIpAddress = serverIpAddress;
            this.serverLanguage = serverLanguage;
        }

        ushort GameServerListRepository.GameServer.ServerEndpoint() => serverEndpoint;
        byte GameServerListRepository.GameServer.ServerFlag() => serverFlag;
        IPAddress GameServerListRepository.GameServer.ServerIpAddress() => serverIpAddress;
        byte GameServerListRepository.GameServer.ServerLanguage() => serverLanguage;
        string GameServerListRepository.GameServer.ServerName() => serverName;
        ushort GameServerListRepository.GameServer.ServerPort() => serverPort;
    }
}
