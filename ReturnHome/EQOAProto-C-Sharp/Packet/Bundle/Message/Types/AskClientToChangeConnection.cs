using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface AskClientToChangeConnection : OpcodeMessage {
        public static readonly ushort OPCODE = 0x0790;

        public static OpcodeMessage Read(PacketBytes packetBytes) => new AskClientToChangeConnection.Impl(
            newEndpoint: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
            newPort: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
            newIpAddress: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
            connectionNumber: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
            clientEndpoint: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
            clientPort: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
            dummyIpAddress: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)));

        private class Impl : AskClientToChangeConnection {
            readonly Uint16Le newEndpoint;
            readonly Uint16Le newPort;
            readonly Uint32Le newIpAddress;
            readonly Uint32Le connectionNumber;
            readonly Uint16Le clientEndpoint;
            readonly Uint16Le clientPort;
            readonly Uint32Le dummyIpAddress;
            readonly Lazy<PacketBytes> bytes;

            public Impl(Uint16Le newEndpoint, Uint16Le newPort, Uint32Le newIpAddress, Uint32Le connectionNumber, Uint16Le clientEndpoint, Uint16Le clientPort, Uint32Le dummyIpAddress) {
                this.newEndpoint = newEndpoint;
                this.newPort = newPort;
                this.newIpAddress = newIpAddress;
                this.connectionNumber = connectionNumber;
                this.clientEndpoint = clientEndpoint;
                this.clientPort = clientPort;
                this.dummyIpAddress = dummyIpAddress;
                this.bytes = new Lazy<PacketBytes>(() => this.newEndpoint.Serialize()
                    .Append(this.newPort.Serialize())
                    .Append(this.newIpAddress.Serialize())
                    .Append(this.connectionNumber.Serialize())
                    .Append(this.clientEndpoint.Serialize())
                    .Append(this.clientPort.Serialize())
                    .Append(this.dummyIpAddress.Serialize()));
            }

            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handlePacket) => throw new NotSupportedException($"Packet Handlers do not handle {typeof(AskClientToChangeConnection).Name} messages, as this is a Server only message");
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: AskClientToChangeConnection.OPCODE, opcodeMessage: this);
        }
    }
}
