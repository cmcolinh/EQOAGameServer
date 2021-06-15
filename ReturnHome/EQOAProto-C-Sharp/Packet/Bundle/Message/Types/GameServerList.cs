using ReturnHome;
using ReturnHome.Packet.Support;
using ReturnHome.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface GameServerList : OpcodeMessage {
        public static readonly ushort OPCODE = 0x07b3;
        IList<GameServer> Servers();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            VariableLengthEncodedInt serverCount = VariableLengthEncodedInt.Read(packetBytes);
            List<GameServer> servers = new List<GameServer>();
            for (long i = 0; i < serverCount.ToLong(); i++) {
                servers.Add(GameServer.Read(packetBytes));
            }
           return new GameServerList.Impl(serverCount, servers);
        }

        public static GameServerList Of(List<GameServerListRepository.GameServer> servers) => new GameServerList.Impl(
            serverCount: VariableLengthEncodedInt.Of(servers.Count),
            servers: servers.Select(s => s.ToBinaryRecord()).ToList());

        private class Impl : GameServerList {
            readonly VariableLengthEncodedInt serverCount;
            readonly IList<GameServer> servers;
            readonly Lazy<PacketBytes> bytes;

            public IList<GameServer> Servers() => servers;
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => throw new NotSupportedException($"Packet Handlers do not handle {typeof(GameServerList).Name} messages, as this is a Server only message");
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: GameServerList.OPCODE, opcodeMessage: this);

            public Impl(VariableLengthEncodedInt serverCount, List<GameServer> servers) {
                this.serverCount = serverCount;
                this.servers = servers.AsReadOnly();
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                PacketBytes packetBytes = this.serverCount.Serialize();
                foreach (GameServer server in servers) {
                    packetBytes = packetBytes.Append(server.Serialize());
                }
                return packetBytes;
            }
        }

        public interface GameServer : BinaryRecord, GameServerListRepository.GameServer {
            public static GameServer Read(PacketBytes packetBytes) {
                Uint32Le serverNameLength = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
                UCS2StringLe serverName = UCS2StringLe.Read(packetBytes.PopFirst(bytes: (int)(serverNameLength.ToUint() * 2)));
                Uint8 serverFlag = Uint8.Read(packetBytes.PopFirst(bytes: 1));
                Uint16Le serverEndpoint = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
                Uint16Le serverPort = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
                Uint32Le serverIpAddress = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
                Uint8 serverLanguage = Uint8.Read(packetBytes.PopFirst(bytes: 1));
                return new GameServer.Impl(serverNameLength, serverName, serverFlag, serverEndpoint, serverPort, serverIpAddress, serverLanguage);
            }

            public static GameServer Of(string serverName, byte serverFlag, ushort serverEndpoint, ushort serverPort, IPAddress serverIpAddress, byte serverLanguage) {
                byte[] ipAddressBytes = serverIpAddress.MapToIPv4().GetAddressBytes();
                //convert to little endian...
                PacketBytes ipAddressPacketBytes = PacketBytes.Of(new List<byte>{ipAddressBytes[3], ipAddressBytes[2], ipAddressBytes[1], ipAddressBytes[0]});
                return new GameServer.Impl(
                    serverNameLength: Uint32Le.Of((uint)serverName.Length),
                    serverName: UCS2StringLe.Of(serverName),
                    serverFlag: Uint8.Of(serverFlag),
                    serverEndpoint: Uint16Le.Of(serverEndpoint),
                    serverPort: Uint16Le.Of(serverPort),
                    serverIpAddress: Uint32Le.Read(ipAddressPacketBytes),
                    serverLanguage: Uint8.Of(serverLanguage));
            }

            private class Impl : GameServer {
                readonly Uint32Le serverNameLength;
                readonly UCS2StringLe serverName;
                readonly Uint8 serverFlag;
                readonly Uint16Le serverEndpoint;
                readonly Uint16Le serverPort;
                readonly Uint32Le serverIpAddress;
                readonly Uint8 serverLanguage;
                readonly Lazy<PacketBytes> bytes;

                public string ServerName() => serverName.ToString();
                public byte ServerFlag() => serverFlag.ToByte();
                public ushort ServerEndpoint() => serverEndpoint.ToUshort();
                public ushort ServerPort() => serverPort.ToUshort();
                public IPAddress ServerIpAddress() => new IPAddress(serverIpAddress.ToUint()).MapToIPv4();
                public byte ServerLanguage() => serverLanguage.ToByte();
                public GameServer ToBinaryRecord() => this;
                public PacketBytes Serialize() => bytes.Value;

                public Impl(Uint32Le serverNameLength, UCS2StringLe serverName, Uint8 serverFlag, Uint16Le serverEndpoint, Uint16Le serverPort, Uint32Le serverIpAddress, Uint8 serverLanguage) {
                    this.serverNameLength = serverNameLength;
                    this.serverName = serverName;
                    this.serverFlag = serverFlag;
                    this.serverEndpoint = serverEndpoint;
                    this.serverPort = serverPort;
                    this.serverIpAddress = serverIpAddress;
                    this.serverLanguage = serverLanguage;
                    this.bytes = new Lazy<PacketBytes>(() => this.serverNameLength.Serialize()
                        .Append(this.serverName.Serialize())
                        .Append(this.serverFlag.Serialize())
                        .Append(this.serverEndpoint.Serialize())
                        .Append(this.serverPort.Serialize())
                        .Append(this.serverIpAddress.Serialize())
                        .Append(this.serverLanguage.Serialize()));
                }
            }
        }
    }
}
