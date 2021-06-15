using ReturnHome;
using ReturnHome.Packet.Support;
using System;
using System.Runtime.Serialization;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface AccountCredentials : OpcodeMessage {
        public static readonly ushort OPCODE = 0x0001;
        public static readonly ushort ALTERNATE_OPCODE = 0x0904;
        string AccountName();
        string EncryptedPassword();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            Uint8 unknown = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            Uint32Le unknown2 = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            Uint32Le gameCodeLength = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            ASCIIString gameCode = ASCIIString.Read(packetBytes.PopFirst(bytes: (int)gameCodeLength.ToUint()));
            if (!gameCode.ToString().Equals("EQOA")) {
                throw new SerializationException("Game must be \"EQOA\"");
            }
            Uint32Le accountNameLength = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            ASCIIString accountName = ASCIIString.Read(packetBytes.PopFirst(bytes: (int)accountNameLength.ToUint()));
            Uint8 unknown3 = Uint8.Read(packetBytes.PopFirst(bytes: 1));
            ASCIIString encryptedPassword = ASCIIString.Read(packetBytes.PopFirst(bytes: 32));
            return new AccountCredentials.Impl(unknown, unknown2, gameCodeLength, gameCode, accountNameLength, accountName, unknown3, encryptedPassword);
        }

        private class Impl : AccountCredentials {
            readonly Uint8 unknown;
            readonly Uint32Le unknown2;
            readonly Uint32Le gameCodeLength;
            readonly ASCIIString gameCode;
            readonly Uint32Le accountNameLength;
            readonly ASCIIString accountName;
            readonly Uint8 unknown3;
            readonly ASCIIString encryptedPassword;
            readonly Lazy<PacketBytes> bytes;

            public Impl(Uint8 unknown, Uint32Le unknown2, Uint32Le gameCodeLength, ASCIIString gameCode, Uint32Le accountNameLength, ASCIIString accountName, Uint8 unknown3, ASCIIString encryptedPassword) {
                this.unknown = unknown;
                this.unknown2 = unknown2;
                this.gameCodeLength = gameCodeLength;
                this.gameCode = gameCode;
                this.accountNameLength = accountNameLength;
                this.accountName = accountName;
                this.unknown3 = unknown3;
                this.encryptedPassword = encryptedPassword;
                this.bytes = new Lazy<PacketBytes>(() => this.unknown.Serialize()
                    .Append(this.unknown2.Serialize())
                    .Append(this.gameCodeLength.Serialize())
                    .Append(this.gameCode.Serialize())
                    .Append(this.accountNameLength.Serialize())
                    .Append(this.accountName.Serialize())
                    .Append(this.unknown3.Serialize())
                    .Append(this.encryptedPassword.Serialize()));
            }

            public string AccountName() => accountName.ToString();
            public string EncryptedPassword() => encryptedPassword.ToString();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessAccountCredentials(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: AccountCredentials.OPCODE, opcodeMessage: this);
        }
    }
}
