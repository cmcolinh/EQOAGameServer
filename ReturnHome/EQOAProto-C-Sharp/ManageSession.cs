using ReturnHome.Packet;
using ReturnHome.Packet.Bundle;
using ReturnHome.Packet.Bundle.Message;
using ReturnHome.Packet.Bundle.Message.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ReturnHome {
    public interface ManageSession : HandlePacket {
        public static readonly Lazy<ManageSession> NullHandler = new Lazy<ManageSession>(() => new HandleNull());
        IPEndPoint IPEndPoint();
        ManageSession QueueAsReliableMessage(OpcodeAndMessage opcodeAndMessage);
        ManageSession QueueAsUnreliableMessage(OpcodeAndMessage opcodeAndMessage);
        ManageSession QueueUpdateMessage(UncompressedUpdateMessage updateMessage, byte channel);
        ManageSession SendPacket();
        bool SessionVerified();
        ManageSession VerifySession();

        public static ManageSession Of(HandleMessage handleMessage, BuildPacket buildPacket, IPEndPoint ipEndPoint, UdpClient udpClient) {
            return new ManageSession.Impl(
                handleMessage: handleMessage,
                buildPacket: buildPacket,
                ipEndPoint: ipEndPoint,
                udpClient: udpClient);
        }
        private class Impl : ManageSession {
            public Impl(HandleMessage handleMessage, BuildPacket buildPacket, IPEndPoint ipEndPoint, UdpClient udpClient) {
                this.handleMessage = handleMessage;
                this.buildPacket = buildPacket;
                this.ipEndPoint = ipEndPoint;
                this.udpClient = udpClient;
                this.unacknowledgedReliableMessages = new Dictionary<ushort, ReliableMessage>();
                this.ticksWithoutAcknowledgement = new Dictionary<ushort, byte>();
                this.channel0x40MessagesReceived = new Dictionary<ushort, UncompressedUpdateMessage>();
                this.updateMessagesSent = new Dictionary<byte, Dictionary<ushort, UncompressedUpdateMessage>>();
                this.sessionVerified = false;
            }
            static readonly Dictionary<ushort, Action<ManageSession.Impl, BundleMessage>> handleMessageType = new Dictionary<ushort, Action<Impl, BundleMessage>> {
                {ReliableMessage.TYPE_OF, (manager, message) => manager.ProcessReliableMessage(message)},
                {UnreliableMessage.TYPE_OF, (manager, message) => manager.ProcessUnreliableMessage(message)},
                {0x40, (manager, message) => manager.ProcessUpdateMessage(message)}
            };
            private HandleMessage handleMessage;
            private BuildPacket buildPacket;
            //private uint sessionIdBase; held by buildPacket
            //private ushort sessionIdUp; held by buildPacket
            private IPEndPoint ipEndPoint;
            private UdpClient udpClient;
            private ushort clientBundleNumber;
            private ushort clientMessageNumber;
            private ushort clientAcknowledgedBundleNumber;
            private ushort clientAcknowledgedMessageNumber;
            private ushort serverBundleNumber;
            private ushort serverMessageNumber;
            private Dictionary<ushort, ReliableMessage> unacknowledgedReliableMessages;
            private Dictionary<ushort, byte> ticksWithoutAcknowledgement;
            private Dictionary<ushort, UncompressedUpdateMessage> channel0x40MessagesReceived;
            private Dictionary<byte, Dictionary<ushort, UncompressedUpdateMessage>> updateMessagesSent;
            private bool sessionVerified;
            public IPEndPoint IPEndPoint() => ipEndPoint;
            public void Process(EQOAPacket packet) {
                packet.Bundles()
                    .ToList()
                    .ForEach(bundle => ProcessBundle(bundle));
            }

            public ManageSession QueueAsReliableMessage(OpcodeAndMessage opcodeAndMessage) {
                serverMessageNumber++;
                ReliableMessage reliableMessage = ReliableMessage.Of(
                    opcodeAndMessage: opcodeAndMessage,
                    messageNumber: serverMessageNumber);
                buildPacket.QueueReliableMessage(reliableMessage);
                unacknowledgedReliableMessages.Add(serverMessageNumber, reliableMessage);
                ticksWithoutAcknowledgement.Add(serverMessageNumber, 0);
                return this;
            }

            public ManageSession QueueAsUnreliableMessage(OpcodeAndMessage opcodeAndMessage) {
                buildPacket.QueueUnreliableMessage(message: UnreliableMessage.Of(opcodeAndMessage: opcodeAndMessage));
                return this;
            }

            public ManageSession QueueUpdateMessage(UncompressedUpdateMessage updateMessage, byte channel) {
                //TODO: implementation
                return this;
            }

            public ManageSession SendPacket() {
                byte[] bytes = buildPacket.Build().Serialize().ToArray();
                if (bytes.Length > 0) {
                    udpClient.Send(bytes, bytes.Length, ipEndPoint);
                }
                IncrementTicksWithoutAcknowledgement();
                return this;
            }

            public bool SessionVerified() => sessionVerified;
            public ManageSession VerifySession() {
                this.sessionVerified = true;
                return this;
            }

            private void IncrementTicksWithoutAcknowledgement() {
                ticksWithoutAcknowledgement.Keys
                    .ToList()
                    .ForEach(key => ticksWithoutAcknowledgement[key] = (byte)(ticksWithoutAcknowledgement[key] + 1));
            }

            private void ProcessBundle(PacketBundle bundle) {
                ushort bundleNumber = bundle.BundleNumber();
                if (bundle.BundleNumber() < clientBundleNumber) {
                    return; //not going to bother with non-current packets
                }
                clientBundleNumber = bundleNumber;
                if (bundle.SessionAckRequested()) {
                    uint sessionId = bundle.SessionIdBase() + (bundle.SessionIdUp() << 16);
                    buildPacket = buildPacket
                        .SessionId(sessionIdBase: bundle.SessionIdBase(), sessionIdUp: bundle.SessionIdUp())
                        .QueueSessionAcknowledgement(bundleNum: clientBundleNumber, messageNum: this.clientMessageNumber);
                }
                if (bundle.HasAcks()) {
                    ProcessReliableAcks(bundle);
                }
                bundle.Messages()
                    .ToList()
                    .ForEach(message => ProcessMessage(message));
            }

            private void ProcessReliableAcks(PacketBundle bundle) {
                this.clientAcknowledgedBundleNumber = Math.Max(this.clientAcknowledgedBundleNumber, bundle.BundleAcknowledged());
                this.clientAcknowledgedMessageNumber = Math.Max(this.clientAcknowledgedMessageNumber, bundle.ReliableMessageAcknowledged());
                this.unacknowledgedReliableMessages.Keys
                    .Where(key => key <= this.clientAcknowledgedMessageNumber)
                    .ToList()
                    .ForEach(key => {
                        unacknowledgedReliableMessages.Remove(key);
                        ticksWithoutAcknowledgement.Remove(key);
                    });
            }

            private void ProcessMessage(BundleMessage message) {
                handleMessageType[message.MessageChannel()](this, message);
            }

            private void ProcessReliableMessage(BundleMessage message) {
                ushort clientMessageNumber = message.MessageNumber();
                buildPacket.QueueReliableMessageAcknowledgement(
                    bundleNum: clientBundleNumber,
                    messageNum: clientMessageNumber);
                this.clientMessageNumber = clientMessageNumber;
                handleMessage.Process(message);
            }

            private void ProcessUnreliableMessage(BundleMessage message) => handleMessage.Process(message);

            private void ProcessUpdateMessage(BundleMessage message) {
                ushort updateMessageNum = message.MessageNumber();
                //TODO: implement decompressing message bytes
                buildPacket.QueueChannel0x40Acknowledgement(
                    bundleNum: clientBundleNumber,
                    reliableMessageNum: clientMessageNumber,
                    updateMessageNum: updateMessageNum);
            }
        }
        private class HandleNull : ManageSession {
            public IPEndPoint IPEndPoint() => null;
            public void Process(EQOAPacket eqoaPacket) {}
            public ManageSession QueueAsReliableMessage(OpcodeAndMessage opcodeAndMessage) => this;
            public ManageSession QueueAsUnreliableMessage(OpcodeAndMessage opcodeAndMessage) => this;
            public ManageSession QueueUpdateMessage(UncompressedUpdateMessage compressedUpdateMessage, byte channel) => this;
            public ManageSession SendPacket() => this;
            public bool SessionVerified() => false;
            public ManageSession VerifySession() => this;
        }
    }
}
