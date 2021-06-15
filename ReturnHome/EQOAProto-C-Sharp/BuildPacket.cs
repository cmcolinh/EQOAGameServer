using ReturnHome.Packet;
using ReturnHome.Packet.Bundle;
using ReturnHome.Packet.Bundle.Message;
using ReturnHome.Packet.Support;
using System.Collections.Generic;
using System.Linq;

namespace ReturnHome {
    /// <summary>
    /// This interface represents a builder
    /// <see> https://en.wikipedia.org/wiki/Builder_pattern </see>
    /// that represents a step by step approach to building an outbound packet,
    /// adding one message and ACK at a time, and finalizing when ready to send
    /// </summary>
    public interface BuildPacket {
        /// <summary> Build an EQOAPacket from the queued messages and ACKS, and dequeue those messages </summary>
        EQOAPacket Build();
        /// <summary> Reset the Builder to the start state, removing the queued ACKs and Mesages

        BuildPacket SessionId(uint sessionIdBase, uint sessionIdUp);

        BuildPacket BundleNum(ushort bundleNum);

        BuildPacket IsHighPhase(bool isHighPhase);

        BuildPacket ShortSessionId(bool shortSessionId);

        BuildPacket ServerIsMaster(bool serverIsMaster);

        /// <summary> Ready a ReliableMessage to be sent, BuildPacket is responsible for assigning message numbers </summary>
        BuildPacket QueueReliableMessage(ReliableMessage message);

        /// <summary> Ready an unreliable message to be sent <summary>
        BuildPacket QueueUnreliableMessage(UnreliableMessage message);

        BuildPacket QueueUpdateMessage(UpdateMessage message);

        /// <summary> Called when a reliable packet ACK is received. </summary>
        BuildPacket QueueSessionAcknowledgement(ushort bundleNum, ushort messageNum);

        /// <summary> Called to ready a reliable message ack to send the client </summary>
        BuildPacket QueueReliableMessageAcknowledgement(ushort bundleNum, ushort messageNum);

        /// <summary> Called to ready a channel 0x40 update message ack for the client  </summary>
        BuildPacket QueueChannel0x40Acknowledgement(ushort bundleNum, ushort reliableMessageNum, ushort updateMessageNum);

        BuildPacket SessionAction(byte sessionAction);

        /// <summary> Instantiate a BuildPacket object, given the source and destination endpoint </summary>
        static BuildPacket Of(ushort sourceEndpoint, ushort destinationEndpoint) {
            return new BuildPacket.Impl(sourceEndpoint: sourceEndpoint, destinationEndpoint: destinationEndpoint);
        }

        private class Impl : BuildPacket {
            ushort sourceEndpoint;
            ushort destinationEndpoint;
            uint sessionIdBase;
            uint sessionIdUp;
            ushort bundleNum;

            bool serverIsMaster;
            bool isHighPhase;
            bool shortSessionId;

            byte sessionAction;
            uint sessionAcknowledgementToSend;
            ushort bundleAcknowledgementToSend;
            ushort reliableMessageAcknowledgementToSend;
            ushort channel0x40AcknowledgementToSend;

            List<BundleMessage> queuedMessages;

            public Impl(ushort sourceEndpoint, ushort destinationEndpoint) {
                this.sourceEndpoint = sourceEndpoint;
                this.destinationEndpoint = destinationEndpoint;
                this.shortSessionId = true;
                this.queuedMessages = new List<BundleMessage>();
            }

            public BuildPacket Clear() {
                lock(this) {
                    queuedMessages = new List<BundleMessage>();
                    sessionAcknowledgementToSend = 0;
                    bundleAcknowledgementToSend = 0;
                    channel0x40AcknowledgementToSend = 0;
                }
                return this;
            }

            public BuildPacket SessionId(uint sessionIdBase, uint sessionIdUp) {
                lock(this) {
                    this.sessionIdBase = sessionIdBase;
                    this.sessionIdUp = sessionIdUp;
                }
                return this;
            }

            public BuildPacket BundleNum(ushort bundleNum) {
                lock(this) {
                    this.bundleNum = bundleNum;
                }
                return this;
            }

            public BuildPacket IsHighPhase(bool isHighPhase) {
                lock(this) {
                    this.isHighPhase = isHighPhase;
                }
                return this;
            }

            public BuildPacket ShortSessionId(bool shortSessionId) {
                lock(this) {
                    this.shortSessionId = shortSessionId;
                }
                return this;
            }

            public  BuildPacket ServerIsMaster(bool serverIsMaster) {
                lock(this) {
                    this.serverIsMaster = serverIsMaster;
                }
                return this;
            }

            public BuildPacket SessionAction(byte sessionAction) {
                lock(this) {
                    this.sessionAction = sessionAction;
                }
                return this;
            }

            public BuildPacket QueueReliableMessage(ReliableMessage message) {
                lock(this) {
                    queuedMessages.Add(BundleMessage.Of(
                        messageChannel: ReliableMessage.TYPE_OF,
                        messageContents: message));
                }
                return this;
            }

            public BuildPacket QueueUnreliableMessage(UnreliableMessage message) {
                lock(this) {
                    queuedMessages.Add(BundleMessage.Of(
                        messageChannel: UnreliableMessage.TYPE_OF,
                        messageContents: message));
                }
                return this;
            }

            public BuildPacket QueueReliableMessageAcknowledgement(ushort bundleNum, ushort messageNum) {
                lock(this) {
                    this.bundleAcknowledgementToSend = bundleNum;
                    this.reliableMessageAcknowledgementToSend = messageNum;
                }
                return this;
            }

            public BuildPacket QueueSessionAcknowledgement(ushort bundleNum, ushort messageNum) {
                lock(this) {
                    this.sessionAcknowledgementToSend = this.sessionIdBase + (this.sessionIdUp << 16);
                    this.bundleAcknowledgementToSend = bundleNum;
                    this.reliableMessageAcknowledgementToSend = messageNum;
                }
                return this;
            }

            public BuildPacket QueueChannel0x40Acknowledgement(ushort bundleNum, ushort reliableMessageNum, ushort updateMessageNum) {
                lock(this) {
                    this.bundleAcknowledgementToSend = bundleNum;
                    this.reliableMessageAcknowledgementToSend = reliableMessageNum;
                    this.channel0x40AcknowledgementToSend = updateMessageNum;
                }
                return this;
            }

            public BuildPacket QueueUpdateMessage(UpdateMessage message) {
                return this;
            }

            public EQOAPacket Build() {
                EQOAPacket packet = null;
                lock(this) {
                    PacketBundle packetBundle = BuildPacketBundle();
                    PacketContents packetContents = PacketContents.Of(
                        sourceEndpoint: sourceEndpoint,
                        destinationEndpoint: destinationEndpoint,
                        packetBundles: PacketBundles.Of(packetBundle));
                    packet = EQOAPacket.Of(packetContents: packetContents);
                }
                return packet;
            }

            private PacketBundle BuildPacketBundle() {
                BundlePayload bundlePayload = BundlePayload.Of(
                    bundleNumber: bundleNum,
                    sessionIdAck: sessionAcknowledgementToSend,
                    lastBundleAck: bundleAcknowledgementToSend,
                    lastMessageAck: reliableMessageAcknowledgementToSend,
                    bundleMessages: queuedMessages);
                BundleContents bundleContents = bundlePayload.ToBundleContents();
                BundleHeader bundleHeader = BuildBundleHeader(bundleContents);
                return PacketBundle.Of(
                    bundleHeader: bundleHeader,
                    bundleContents: bundleContents);
            }

            private BundleHeader BuildBundleHeader(BundleContents bundleContents) {
                ushort bundleLength = (ushort)bundleContents.Serialize().Count();
                BundleTypeAndLength bundleTypeAndLength = BundleTypeAndLength.Of(
                    isHighPhase: isHighPhase,
                    serverIsMaster: serverIsMaster,
                    shortSessionId: shortSessionId,
                    sessionAction: sessionAction,
                    bundleLength: bundleLength);
                SessionInfo sessionInfo = SessionInfo.Of(
                    shortSessionId: shortSessionId,
                    sessionIdBase: sessionIdBase,
                    sessionIdUp: sessionIdUp);
                return BundleHeader.Of(
                    bundleTypeAndLength: bundleTypeAndLength,
                    sessionInfo: sessionInfo);
            }
        }
    }
}
