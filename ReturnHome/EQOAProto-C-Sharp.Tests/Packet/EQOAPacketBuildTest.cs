using ReturnHome.Repository;
using ReturnHome.Packet;
using ReturnHome.Packet.Bundle.Message;
using ReturnHome.Packet.Bundle.Message.Types;
using System.Collections.Generic;
using Xunit;

namespace ReturnHome.Tests.Packet {
    public class EQOAPacketBuildTest {
        [Fact]
        public void Packet156SendGameVersionAfterFirstConnect()
        {
            BuildPacket buildPacket = BuildPacket.Of(sourceEndpoint: 0x73b0, destinationEndpoint: 0xe75a)
                .SessionId(sessionIdBase: 0xe75a, sessionIdUp: 0x0005)
                .IsHighPhase(true)
                .ServerIsMaster(false)
                .BundleNum(1)
                .QueueSessionAcknowledgement(bundleNum: 1, messageNum: 2)
                .QueueReliableMessage(ReliableMessage.Of(
                    opcodeAndMessage: DiscVersion.Of(version: 0x25).ToOpcodeAndMessage(),
                    messageNumber: 1));

            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{
                0xb0, 0x73, 0x5a, 0xe7, 0x95, 0x60, 0x5a, 0xe7, 0x05, 0x00, 0x63, 0x5a, 0xe7, 0x05, 0x00, 0x01,
                0x00, 0x01, 0x00, 0x02, 0x00, 0xfb, 0x06, 0x01, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00, 0x00, 0x7c,
                0x69, 0xb6, 0x9e
            });

            EQOAPacket packet = buildPacket.Build();
            PacketBytes actualBytes = packet.Serialize();

            Assert.Equal(actualBytes, expectedBytes);
        }

        [Fact]
        public void Packet158SendServerList()
        {
            MockGameServerListRepository repository = new MockGameServerListRepository();
            List<GameServerListRepository.GameServer> serverList = repository.ServerListFor("name", "uuid");

            BuildPacket buildPacket = BuildPacket.Of(sourceEndpoint: 0x73b0, destinationEndpoint: 0xe75a)
                .SessionId(sessionIdBase: 0xe75a, sessionIdUp: 0x0005)
                .IsHighPhase(true)
                .ServerIsMaster(false)
                .BundleNum(2)
                .QueueUnreliableMessage(UnreliableMessage.Of(
                    opcodeAndMessage: GameServerList.Of(serverList).ToOpcodeAndMessage()));

            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{
                0xb0, 0x73, 0x5a, 0xe7, 0x9c, 0x62, 0x5a, 0xe7, 0x05, 0x00, 0x20, 0x02, 0x00, 0xfc, 0xff, 0x15,
                0x01, 0xb3, 0x07, 0x0e, 0x10, 0x00, 0x00, 0x00, 0x43, 0x00, 0x61, 0x00, 0x73, 0x00, 0x74, 0x00,
                0x6c, 0x00, 0x65, 0x00, 0x20, 0x00, 0x4c, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00, 0x74, 0x00,
                0x77, 0x00, 0x6f, 0x00, 0x6c, 0x00, 0x66, 0x00, 0x00, 0x0a, 0x1f, 0x57, 0x27, 0x30, 0x0a, 0x6c,
                0xc7, 0x00, 0x0a, 0x00, 0x00, 0x00, 0x44, 0x00, 0x69, 0x00, 0x72, 0x00, 0x65, 0x00, 0x6e, 0x00,
                0x20, 0x00, 0x48, 0x00, 0x6f, 0x00, 0x6c, 0x00, 0x64, 0x00, 0x00, 0x01, 0x3e, 0x56, 0x27, 0x48,
                0x0a, 0x6c, 0xc7, 0x00, 0x0d, 0x00, 0x00, 0x00, 0x46, 0x00, 0x65, 0x00, 0x72, 0x00, 0x72, 0x00,
                0x61, 0x00, 0x6e, 0x00, 0x27, 0x00, 0x73, 0x00, 0x20, 0x00, 0x48, 0x00, 0x6f, 0x00, 0x70, 0x00,
                0x65, 0x00, 0x00, 0x1c, 0x24, 0x56, 0x27, 0x74, 0x0a, 0x6c, 0xc7, 0x00, 0x08, 0x00, 0x00, 0x00,
                0x48, 0x00, 0x6f, 0x00, 0x64, 0x00, 0x73, 0x00, 0x74, 0x00, 0x6f, 0x00, 0x63, 0x00, 0x6b, 0x00,
                0x00, 0x91, 0xb9, 0x56, 0x27, 0x84, 0x0a, 0x6c, 0xc7, 0x00, 0x0b, 0x00, 0x00, 0x00, 0x4d, 0x00,
                0x61, 0x00, 0x72, 0x00, 0x72, 0x00, 0x27, 0x00, 0x73, 0x00, 0x20, 0x00, 0x46, 0x00, 0x69, 0x00,
                0x73, 0x00, 0x74, 0x00, 0x00, 0x9e, 0xbd, 0x57, 0x27, 0x25, 0xc8, 0x6c, 0xc7, 0x00, 0x11, 0x00,
                0x00, 0x00, 0x50, 0x00, 0x72, 0x00, 0x6f, 0x00, 0x75, 0x00, 0x64, 0x00, 0x70, 0x00, 0x69, 0x00,
                0x6e, 0x00, 0x65, 0x00, 0x20, 0x00, 0x4f, 0x00, 0x75, 0x00, 0x74, 0x00, 0x70, 0x00, 0x6f, 0x00,
                0x73, 0x00, 0x74, 0x00, 0x00, 0xa9, 0xd7, 0x56, 0x27, 0x44, 0xc8, 0x6c, 0xc7, 0x00, 0x0d, 0x00,
                0x00, 0x00, 0x48, 0x00, 0x61, 0x00, 0x67, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x79, 0x00, 0x20, 0x00,
                0x28, 0x00, 0x54, 0x00, 0x65, 0x00, 0x73, 0x00, 0x74, 0x00, 0x29, 0x00, 0x01, 0x35, 0x9b, 0x56,
                0x27, 0xeb, 0x0a, 0x6c, 0xc7, 0x00, 0xc6, 0x2e, 0x9e, 0x70
            });

            EQOAPacket packet = buildPacket.Build();
            PacketBytes actualBytes = packet.Serialize();

            Assert.Equal(actualBytes, expectedBytes);
        }

        [Fact]
        public void Packet166InitializeCharacterSelectScreen() {
            BuildPacket buildPacket = BuildPacket.Of(sourceEndpoint: 0x1f0a, destinationEndpoint: 0xe75a)
                .SessionId(sessionIdBase: 0x133246f1, sessionIdUp: 110380)
                .SessionAction(0x21)
                .IsHighPhase(true)
                .ShortSessionId(false)
                .ServerIsMaster(false)
                .BundleNum(1)
                .QueueReliableMessage(ReliableMessage.Of(
                    opcodeAndMessage: Message0x07d1InitializingCharacterSelect.Of(value: 0x03).ToOpcodeAndMessage(),
                    messageNumber: 1))
                .QueueReliableMessage(ReliableMessage.Of(
                    opcodeAndMessage: Message0x07f5InitializingCharacterSelect.Of(value: 0x1b).ToOpcodeAndMessage(),
                    messageNumber: 2));

            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{
                0x0a, 0x1f, 0x5a, 0xe7, 0x97, 0xc0, 0x21, 0xf1, 0x46, 0x32, 0x13, 0xd8, 0xbc, 0x0d, 0x20, 0x01,
                0x00, 0xfb, 0x06, 0x01, 0x00, 0xd1, 0x07, 0x03, 0x00, 0x00, 0x00, 0xfb, 0x06, 0x02, 0x00, 0xf5,
                0x07, 0x1b, 0x00, 0x00, 0x00, 0xc4, 0xd6, 0x0f, 0xa3
            });

            EQOAPacket packet = buildPacket.Build();
            PacketBytes actualBytes = packet.Serialize();

            Assert.Equal(actualBytes, expectedBytes);
        }

        [Fact]
        public void Packet169SendCharacterList() {
            CharacterRepository repository = new MockCharacterRepository();
            List<CharacterRepository.ViewingModel> characterList = repository.ViewingModelFor(userName: "kieshaesha");

            BuildPacket buildPacket = BuildPacket.Of(sourceEndpoint: 0x1f0a, destinationEndpoint: 0xe75a)
                .SessionId(sessionIdBase: 0x133246f1, sessionIdUp: 110380)
                .SessionAction(0x01)
                .IsHighPhase(true)
                .ShortSessionId(false)
                .ServerIsMaster(false)
                .BundleNum(2)
                .QueueReliableMessage(ReliableMessage.Of(
                    opcodeAndMessage: CharacterViewing.Of(characters: characterList).ToOpcodeAndMessage(),
                    messageNumber: 3));

            PacketBytes expectedBytes = PacketBytes.Of(new List<byte>{
                0x0a, 0x1f, 0x5a, 0xe7, 0xa0, 0xc6, 0x01, 0xf1, 0x46, 0x32, 0x13, 0xd8, 0xbc, 0x0d, 0x20, 0x02,
                0x00, 0xfb, 0xff, 0x17, 0x03, 0x03, 0x00, 0x2c, 0x00, 0x10, 0x05, 0x00, 0x00, 0x00, 0x46, 0x65,
                0x72, 0x72, 0x79, 0x90, 0xa0, 0x8b, 0x01, 0xbe, 0xfb, 0xd1, 0x9c, 0x0c, 0x10, 0x0c, 0x78, 0x00,
                0x00, 0x04, 0x06, 0x00, 0x00, 0x00, 0x00, 0x85, 0x6c, 0x40, 0xd8, 0xcb, 0x7d, 0x7e, 0xbf, 0x00,
                0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x03, 0x00, 0x03, 0x03, 0x03, 0x07, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x80,
                0x00, 0xff, 0xf0, 0xf0, 0x00, 0xff, 0x00, 0x80, 0x00, 0xff, 0x08, 0x00, 0x00, 0x00, 0x44, 0x61,
                0x79, 0x64, 0x72, 0x69, 0x66, 0x74, 0xc6, 0xbb, 0x8b, 0x01, 0x8f, 0xc0, 0xd4, 0xf2, 0x04, 0x18,
                0x02, 0x78, 0x00, 0x04, 0x02, 0x02, 0x00, 0x00, 0x00, 0x00, 0xa9, 0xbe, 0x7d, 0x7c, 0x00, 0x00,
                0x00, 0x00, 0x29, 0x60, 0xc5, 0x2e, 0x03, 0x00, 0x00, 0x01, 0x00, 0x01, 0x01, 0x01, 0x01, 0x00,
                0x00, 0x02, 0x03, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x20, 0x39, 0x64,
                0xff, 0x80, 0x00, 0x80, 0xff, 0xff, 0xff, 0xff, 0xff, 0x3f, 0x76, 0x94, 0xff, 0x80, 0x00, 0x80,
                0xff, 0x75, 0x00, 0x75, 0xff, 0x44, 0x44, 0x88, 0xff, 0xff, 0x00, 0x00, 0xff, 0x04, 0x00, 0x00,
                0x00, 0x4c, 0x65, 0x61, 0x72, 0xfa, 0xd5, 0x8b, 0x01, 0x8f, 0xc0, 0xd4, 0xf2, 0x04, 0x0e, 0x02,
                0x78, 0x02, 0x06, 0x02, 0x02, 0x00, 0x00, 0x00, 0x00, 0x85, 0x6c, 0x40, 0xd8, 0x00, 0x00, 0x00,
                0x00, 0x73, 0xe8, 0xa6, 0x0a, 0x03, 0x00, 0x00, 0x02, 0x00, 0x02, 0x02, 0x02, 0x02, 0x00, 0x00,
                0x03, 0x03, 0x03, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x50, 0x64, 0x00, 0xff,
                0x00, 0x40, 0x40, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x40, 0x40, 0xff, 0x00, 0x40, 0x40, 0xff,
                0x00, 0x40, 0x40, 0xff, 0x50, 0x64, 0x00, 0xff, 0x00, 0x80, 0x00, 0xff, 0x07, 0x00, 0x00, 0x00,
                0x4b, 0x65, 0x6e, 0x63, 0x61, 0x64, 0x65, 0xc4, 0x89, 0x8d, 0x01, 0x8f, 0xc0, 0xd4, 0xf2, 0x04,
                0x12, 0x02, 0x78, 0x00, 0x06, 0x06, 0x02, 0x00, 0x00, 0x00, 0x00, 0x85, 0x6c, 0x40, 0xd8, 0xa8,
                0xe4, 0x2c, 0x99, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x04, 0x00, 0x04, 0x04, 0x04, 0x04,
                0x01, 0x00, 0x03, 0x00, 0x03, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0x1b, 0xed, 0x59, 0xff, 0xff, 0xff, 0xff, 0xff, 0x79, 0x79, 0xbd, 0xff, 0x2d, 0x84,
                0x3c, 0xff, 0x79, 0x79, 0xbd, 0xff, 0x9e, 0x9c, 0x38, 0xff, 0x00, 0x80, 0x00, 0xff, 0x0b, 0x00,
                0x00, 0x00, 0x48, 0x79, 0x6d, 0x6e, 0x6f, 0x66, 0x70, 0x6f, 0x77, 0x65, 0x72, 0xf2, 0x81, 0x93,
                0x01, 0x8f, 0xc0, 0xd4, 0xf2, 0x04, 0x0a, 0x02, 0x78, 0x00, 0x06, 0x04, 0x02, 0x01, 0x00, 0x00,
                0x00, 0x0b, 0x87, 0xf9, 0x17, 0x1a, 0x99, 0xdd, 0xf4, 0x00, 0x00, 0x00, 0x00, 0x01, 0x05, 0x00,
                0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x03, 0x00, 0x02, 0x03, 0x03, 0x00, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x80, 0xff, 0xd6, 0xff, 0x00, 0x9f, 0x9f, 0xff,
                0x00, 0x41, 0x82, 0xff, 0x00, 0x9f, 0x9f, 0xff, 0x00, 0x80, 0xc0, 0xff, 0xff, 0xff, 0xff, 0xff,
                0x15, 0x00, 0x00, 0xff, 0x06, 0x00, 0x00, 0x00, 0x4e, 0x65, 0x63, 0x6e, 0x6f, 0x6b, 0xac, 0xc3,
                0x94, 0x01, 0xd5, 0xd3, 0x9c, 0xe6, 0x0a, 0x16, 0x06, 0x78, 0x06, 0x06, 0x02, 0x00, 0x00, 0x00,
                0x00, 0x00, 0xa9, 0xbe, 0x7d, 0x7c, 0x7c, 0x8b, 0x08, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00,
                0x00, 0x01, 0x00, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x96, 0x00, 0x3c, 0xff, 0xff, 0xff, 0xff,
                0xff, 0x96, 0x00, 0x3c, 0xff, 0x96, 0x00, 0x3c, 0xff, 0x31, 0x62, 0x62, 0xff, 0x15, 0x00, 0x00,
                0xff, 0xff, 0x00, 0x00, 0xff, 0x07, 0x00, 0x00, 0x00, 0x44, 0x75, 0x64, 0x64, 0x65, 0x72, 0x7a,
                0xf6, 0xc3, 0x95, 0x01, 0xe4, 0x94, 0xfd, 0xc6, 0x09, 0x00, 0x0a, 0x78, 0x00, 0x06, 0x00, 0x04,
                0x00, 0x00, 0x00, 0x00, 0x4d, 0x22, 0xb7, 0x6e, 0x0b, 0x87, 0xf9, 0x17, 0x00, 0x00, 0x00, 0x00,
                0x01, 0x01, 0x00, 0x04, 0x00, 0x04, 0x04, 0x04, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x62, 0x00, 0x00, 0xff, 0xff,
                0xff, 0xff, 0xff, 0x40, 0x00, 0x80, 0xff, 0xa0, 0xaa, 0xb4, 0xff, 0x40, 0x00, 0x80, 0xff, 0x40,
                0x00, 0x80, 0xff, 0x00, 0x00, 0xff, 0xff, 0x0c, 0x00, 0x00, 0x00, 0x43, 0x6f, 0x72, 0x73, 0x74,
                0x65, 0x6e, 0x73, 0x62, 0x61, 0x6e, 0x6b, 0xba, 0xef, 0x99, 0x01, 0x8c, 0xe3, 0xc4, 0x8d, 0x0e,
                0x14, 0x00, 0x02, 0x02, 0x04, 0x04, 0x04, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x69, 0x3f,
                0x35, 0x6c
            });

            EQOAPacket packet = buildPacket.Build();
            PacketBytes actualBytes = packet.Serialize();

            Assert.Equal(actualBytes, expectedBytes);
        }
    }
}

