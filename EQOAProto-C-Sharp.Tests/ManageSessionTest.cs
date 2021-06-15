using Xunit;
using EQOAProto_C_Sharp.PacketHandler;
using EQOAProto_C_Sharp.PacketHandler.HandleFirstPacket;
using EQOAProto_C_Sharp.Repository;
using Packet;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace EQOAProto_C_Sharp.UnitTests.Packet {
    public class ManageSessionTest {
        [Fact]
        public void T()
        {
            HandleMessageDelegator handleMessageDelegator = HandleMessageDelegator.NewInstance();
            BuildPacket buildPacket = BuildPacket.Of(sourceEndpoint: 0x73B0, destinationEndpoint: 0xE75A)
                .IsHighPhase(true);
            ManageSession manageSession = ManageSession.Of(
                handleMessage: handleMessageDelegator,
                buildPacket: buildPacket,
                ipEndPoint: new IPEndPoint(0,0),
                udpClient: new UdpClient(7483));
            HandleAccountCredentialsMessage handleAccountCredentalsMessage = HandleAccountCredentialsMessage.Of(
                accountRepository: new MockAccountRepository(),
                handleMessageDelegator: handleMessageDelegator,
                nextHandler: HandleMessage.NullHandler.Value,
                findSessionForClientEndpoint: FindSessionForClientEndpoint.NullHandler.Value,
                manageSession: manageSession);
            HandleGameVersionMessage handleGameVersionMessage = HandleGameVersionMessage.Of(
                expectedDiscVersion: 0x25,
                handleMessageDelegator: handleMessageDelegator,
                handleAccountCredentialsMessage: handleAccountCredentalsMessage);
            handleMessageDelegator.Delegate(handleGameVersionMessage);

            EQOAPacket packet155 = Packet155();

            manageSession.Process(packet155);
            PacketBytes expectedBytes = Packet156().Serialize();

            EQOAPacket packet = buildPacket.BundleNum(1).Build();
            PacketBytes actualBytes = packet.Serialize();
            Assert.Equal(actualBytes, expectedBytes);
        }

        private EQOAPacket Packet155() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{
                0x5a, 0xe7, 0xfe, 0xff, 0xcf, 0xe0, 0x21, 0x5a, 0xe7, 0x05, 0x00, 0x20, 0x01, 0x00, 0xfb, 0x06,
                0x01, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00, 0x00, 0xfb, 0x3e, 0x02, 0x00, 0x04, 0x09, 0x00, 0x03,
                0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x45, 0x51, 0x4f, 0x41, 0x0a, 0x00, 0x00, 0x00, 0x6b,
                0x69, 0x65, 0x73, 0x68, 0x61, 0x65, 0x73, 0x68, 0x61, 0x01, 0xfa, 0x10, 0x69, 0x22, 0x1c, 0xd4,
                0x45, 0xbc, 0xfd, 0x68, 0x3c, 0x56, 0x22, 0x87, 0xd9, 0x70, 0xb7, 0x1c, 0x12, 0xae, 0x76, 0xc4,
                0x98, 0xfd, 0xf3, 0xce, 0xeb, 0x44, 0x4a, 0x0a, 0x49, 0xb5, 0xf6, 0xbe, 0x24, 0xe4
            });
            return EQOAPacket.Read(packetBytes);
        }

        private EQOAPacket Packet156() {
            PacketBytes packetBytes = PacketBytes.Of(new List<byte>{
                0xb0, 0x73, 0x5a, 0xe7, 0x95, 0x60, 0x5a, 0xe7, 0x05, 0x00, 0x63, 0x5a, 0xe7, 0x05, 0x00, 0x01,
                0x00, 0x01, 0x00, 0x02, 0x00, 0xfb, 0x06, 0x01, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00, 0x00, 0x7c,
                0x69, 0xb6, 0x9e
            });
            return EQOAPacket.Read(packetBytes);
        }
    }

    public class MockAccountRepository : AccountRepository {
        public bool CredentialsAreValid(string accountName, string credentials) => true;
    }
}
