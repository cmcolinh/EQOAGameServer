using ReturnHome;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle.Message {
    public interface MessageContents : BinaryRecord {
        ushort MessageNumber();
        ushort MessageLen();
        void Accept(HandleMessage handleMessage);
        BundleMessage ToBundleMessage(byte messageChannel);

        private static readonly Dictionary<byte, Func<PacketBytes, MessageContents>> GetMessageContentsFor = new Dictionary<byte, Func<PacketBytes, MessageContents>> {
            {0x00, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x01, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x02, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x03, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x04, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x05, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x06, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x07, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x08, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x09, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0a, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0b, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0c, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0d, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0e, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x0f, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x10, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x11, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x12, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x13, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x14, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x15, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x16, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x17, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x40, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x42, packetBytes => UpdateMessage.Read(packetBytes)},
            {0x43, packetBytes => UpdateMessage.Read(packetBytes)},
            {0xfb, packetBytes => ReliableMessage.Read(packetBytes)},
            {0xfc, packetBytes => UnreliableMessage.Read(packetBytes)},
            {0xff, packetBytes => TransferMessage.Read(packetBytes)}
        };

        public static MessageContents Read(PacketBytes packetBytes, byte selection) {
            byte messageType = selection;
            return GetMessageContentsFor[messageType](packetBytes);
        }
    }
}
