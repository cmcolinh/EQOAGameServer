using ReturnHome;
using ReturnHome.Packet.Support;
using System;
using System.Net;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface IntermediateTransferConnection : OpcodeMessage {
        public static readonly ushort OPCODE = 0x0992;
        uint TransferNumber();
        ushort ClientEndpoint();
        ushort ClientPort();
        IPAddress DummyIpAddress();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint32Le transferNumber = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint16Le clientEndpoint = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint16Le clientPort = Uint16Le.Read(packetBytes.PopFirst(bytes: 2));
            Uint32Le dummyIpAddress = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new IntermediateTransferConnection.Impl(
                transferNumber: transferNumber,
                clientEndpoint: clientEndpoint,
                clientPort: clientPort,
                dummyIpAddress: dummyIpAddress);
        }

        private class Impl : IntermediateTransferConnection {
            readonly Uint32Le transferNumber;
            readonly Uint16Le clientEndpoint;
            readonly Uint16Le clientPort;
            readonly Uint32Le dummyIpAddress;
            readonly Lazy<PacketBytes> bytes;

            public Impl(Uint32Le transferNumber, Uint16Le clientEndpoint, Uint16Le clientPort, Uint32Le dummyIpAddress) {
                this.transferNumber = transferNumber;
                this.clientEndpoint = clientEndpoint;
                this.clientPort = clientPort;
                this.dummyIpAddress = dummyIpAddress;
                this.bytes = new Lazy<PacketBytes>(() => this.transferNumber.Serialize()
                    .Append(this.clientEndpoint.Serialize())
                    .Append(this.clientPort.Serialize())
                    .Append(this.dummyIpAddress.Serialize()));
            }

            public uint TransferNumber() => transferNumber.ToUint();
            public ushort ClientEndpoint() => clientEndpoint.ToUshort();
            public ushort ClientPort() => clientPort.ToUshort();
            public IPAddress DummyIpAddress() => new IPAddress(dummyIpAddress.ToUint()).MapToIPv4();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessIntermediateTransferConnection(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: IntermediateTransferConnection.OPCODE, opcodeMessage: this);
        }
    }
}
