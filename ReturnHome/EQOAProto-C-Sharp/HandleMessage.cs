using ReturnHome.Packet.Bundle;
using ReturnHome.Packet.Bundle.Message;
using ReturnHome.Packet.Bundle.Message.Types;
using System;

namespace ReturnHome {
    public interface HandleMessage {
        public static readonly Lazy<HandleMessage> NullHandler = new Lazy<HandleMessage>(() => new NullMessageHandler());

        /// <summary> handles an EQOA packet bundle, processing the ACKS and messages, and carrying out appropriate actions and responses </summary>
        void Process(BundleMessage bundleMessage) => bundleMessage.Accept(this);
        /// <summary> handles opcode 0x0000 message (DiscVersion) </summary>
        void ProcessDiscVersion(DiscVersion message) => throw new NotSupportedException("This Packet Handler cannot handle DiscVersion messages");
        /// <summary> handles opcode 0x0001 and 0x0904 message (AccountCredentials) </summary>
        void ProcessAccountCredentials(AccountCredentials message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handle opcode 0x000e message </summary>
        void ProcessMessage0x0e(Message0x0e message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handle opcode 0x0014 message </summary>
        void ProcessMessage0x14(Message0x14 message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handle opcode 0x00c9 message </summary>
        void ProcessMessage0xc9(Message0xc9 message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handle opcode 0x00ca message </summary>
        void ProcessMessage0xca(Message0xca message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles opcode 0x002a message (CharacterSelect) </summary>
        void ProcessCharacterSelect(CharacterSelect message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles opcode 0x002b message (CharacterCreation) </summary>
        void ProcessCharacterCreation(CharacterCreation message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles opcode 0x002b message (CharacterCreation) </summary>
        void ProcessCharacterViewing(CharacterViewing message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles opcode 0x002d message (CharacterDeletion) </summary>
        void ProcessCharacterDeletion(CharacterDeletion message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles opcode 0x0992 message (IntermediateTransferConnection) </summary>
        void ProcessIntermediateTransferConnection(IntermediateTransferConnection message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");
        /// <summary> handles session action 0x14 requests to close session </summary>
        void ProcessCloseSession(CloseSession closeSession) => throw new NotSupportedException($"This Packet Handler cannot handle {closeSession.GetType()} messages");
        /// <summary> handle compressed player update message (the so called 4029 messages) </summary>
        void HandleUpdateMessage(UpdateMessage message) => throw new NotSupportedException($"This Packet Handler cannot handle {message.GetType()} messages");

        private class NullMessageHandler : HandleMessage {
            void Process(BundleMessage bundleMessage) => throw new NotSupportedException("This Packet Handler cannot handle messages");
        }
    }
}
