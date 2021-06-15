using ReturnHome.Packet.Bundle.Types;
using System;
using System.Collections.Generic;

namespace ReturnHome.Packet.Bundle {
    public interface BundlePayload : BinaryRecord {
        ushort BundleNumber();
        ushort BundleAcknowledged();
        ushort ReliableMessageAcknowledged();
        BundleContents ToBundleContents();
        bool HasAcks();
        IList<BundleMessage> Messages();

        private static readonly Dictionary<byte, Func<PacketBytes, BundlePayload>> GetBundleContentsTypeFor = new Dictionary<byte, Func<PacketBytes, BundlePayload>> {
            {ProcessAll.TYPE_OF, packetBytes => ProcessAll.Read(packetBytes)},
            {ProcessReport.TYPE_OF, packetBytes => ProcessReport.Read(packetBytes)},
            {ProcessMessages.TYPE_OF, packetBytes => ProcessMessages.Read(packetBytes)},
            {ProcessUpdateReport.TYPE_OF, packetBytes => ProcessUpdateReport.Read(packetBytes)},
            {ProcessMessagesAndReport.TYPE_OF, packetBytes => ProcessMessagesAndReport.Read(packetBytes)},
            {NewProcessReport.TYPE_OF, packetBytes => NewProcessReport.Read(packetBytes)},
            {NewProcessMessages.TYPE_OF, packetBytes => NewProcessMessages.Read(packetBytes)}
        };

        private static readonly Dictionary<Tuple<bool, bool, bool>, Func<uint, ushort, ushort, ushort, List<BundleMessage>, BundlePayload>> BuildBundlePayloadFor = new Dictionary<Tuple<bool, bool, bool>, Func<uint, ushort, ushort, ushort, List<BundleMessage>, BundlePayload>>{
            {new Tuple<bool, bool, bool>(true, true, true), (sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages) => ProcessAll.Of(sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages)},
            {new Tuple<bool, bool, bool>(true, true, false), (sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages) => ProcessAll.Of(sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages)},
            {new Tuple<bool, bool, bool>(false, true, false), (sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages) => ProcessReport.Of(bundleNumber, lastBundleAck, lastMessageAck)},
            {new Tuple<bool, bool, bool>(false, false, true), (sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages) => ProcessMessages.Of(bundleNumber, bundleMessages)},
            {new Tuple<bool, bool, bool>(false, true, true), (sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages) => ProcessMessagesAndReport.Of(bundleNumber, lastBundleAck, lastMessageAck, bundleMessages)}
        };

        public static BundlePayload Read(PacketBytes packetBytes, byte selection) {
            byte bundleType = selection;
            return GetBundleContentsTypeFor[bundleType](packetBytes);
        }

        public static BundlePayload Of(uint sessionIdAck, ushort bundleNumber, ushort lastBundleAck, ushort lastMessageAck , List<BundleMessage> bundleMessages) {
            Tuple<bool, bool, bool> flags = new Tuple<bool, bool, bool>(
                sessionIdAck != 0, //hasSessionAck
                lastBundleAck != 0, //hasReliableMessageAck
                bundleMessages.Count > 0); //hasMessages
            return BuildBundlePayloadFor[flags](sessionIdAck, bundleNumber, lastBundleAck, lastMessageAck, bundleMessages);
        }
    }
}
