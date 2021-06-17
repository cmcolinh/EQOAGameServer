using ReturnHome.Repository;
using ReturnHome.Packet.Bundle.Message.Types;
using System;

namespace ReturnHome.PacketHandler {
    namespace HandleFirstPacket {
        public class HandleGameVersionMessage : HandleMessage {
            public static HandleGameVersionMessage Of(uint expectedDiscVersion, HandleMessageDelegator handleMessageDelegator, HandleAccountCredentialsMessage handleAccountCredentialsMessage) {
                return new HandleGameVersionMessage(
                    expectedDiscVersion: expectedDiscVersion,
                    handleMessageDelegator: handleMessageDelegator,
                    handleAccountCredentialsMessage: handleAccountCredentialsMessage);
            }
            private HandleGameVersionMessage(uint expectedDiscVersion, HandleMessageDelegator handleMessageDelegator, HandleAccountCredentialsMessage handleAccountCredentialsMessage) {
                this.expectedDiscVersion = expectedDiscVersion;
                this.handleMessageDelegator = handleMessageDelegator;
                this.handleAccountCredentialsMessage = handleAccountCredentialsMessage;
            }
            readonly uint expectedDiscVersion;
            readonly HandleMessageDelegator handleMessageDelegator;
            readonly HandleAccountCredentialsMessage handleAccountCredentialsMessage;

            public void ProcessDiscVersion(DiscVersion message) {
                if (message.Version() == expectedDiscVersion) {
                    handleMessageDelegator.Delegate(to: handleAccountCredentialsMessage);
                }
            }
        }
        public class HandleAccountCredentialsMessage : HandleMessage {
            public static HandleAccountCredentialsMessage Of(AccountRepository accountRepository, HandleMessageDelegator handleMessageDelegator, HandleMessage nextHandler, ManageSession manageSession, FindSessionForClientEndpoint findSessionForClientEndpoint) {
                return new HandleAccountCredentialsMessage(
                    accountRepository: accountRepository,
                    handleMessageDelegator: handleMessageDelegator,
                    nextHandler: nextHandler,
                    manageSession: manageSession,
                    findSessionForClientEndpoint);
            }
            private HandleAccountCredentialsMessage(AccountRepository accountRepository, HandleMessageDelegator handleMessageDelegator, HandleMessage nextHandler, ManageSession manageSession, FindSessionForClientEndpoint findSessionForClientEndpoint) {
                this.accountRepository = accountRepository;
                this.handleMessageDelegator = handleMessageDelegator;
                this.nextHandler = nextHandler;
                this.manageSession = manageSession;
                this.findSessionForClientEndpoint = findSessionForClientEndpoint;
            }
            readonly AccountRepository accountRepository;
            readonly HandleMessageDelegator handleMessageDelegator;
            readonly HandleMessage nextHandler;
            readonly ManageSession manageSession;
            readonly FindSessionForClientEndpoint findSessionForClientEndpoint;
            readonly ushort clientEndpoint;
            public void ProcessAccountCredentials(AccountCredentials message) {
                if (accountRepository.CredentialsAreValid(accountName: message.AccountName(), credentials: message.EncryptedPassword())) {
                    //continue processing session
                    handleMessageDelegator.Delegate(nextHandler);
                    //register this session manager with the session established endpoint
                    findSessionForClientEndpoint.AddHandler(clientEndpoint, manageSession);
                    //queue the response to the message
                    manageSession.QueueAsReliableMessage(DiscVersion.Of(DiscVersion.FRONTIERS).ToOpcodeAndMessage());
                } else {
                    throw new ArgumentException("Credentials not accepted");
                }
            }
        }
    }
}
