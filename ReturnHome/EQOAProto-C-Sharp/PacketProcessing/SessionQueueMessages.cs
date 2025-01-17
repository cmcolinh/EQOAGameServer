﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using ReturnHome.Utilities;
using ReturnHome.Opcodes;

namespace ReturnHome.PacketProcessing
{
    public class SessionQueueMessages
    {
        public MessageCreator messageCreator = new();

        public SessionQueueMessages()
        {
        }

        ///Message processing for outbound section
        public void PackMessage(Session MySession, byte MessageOpcodeType)
        {
            //This is only needed so often
            int readBytes = 0;
            ReadOnlyMemory<byte> ClientMessage = messageCreator.MessageReader();
            //Check if message will span multiple packets
            if (ClientMessage.Length >= 1024)
            {
                while ((ClientMessage.Length - readBytes) >= 1024)
                {
                    MessageHeaderReliableLong thisMessageHeader = new(MessageOpcodeTypes.MultiLongReliableMessage, 1024, MySession.ServerMessageNumber);

                    Memory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 6];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..6]);
                    ClientMessage[readBytes..(readBytes + 1024)].CopyTo(WholeClientMessage[6..WholeClientMessage.Length]);
                    readBytes += 1024;
                    AddMessage(MySession, MySession.ServerMessageNumber, WholeClientMessage);

                    //Increment server message #
                    MySession.IncrementServerMessageNumber();
                }

                //Slice remaining bytes left to put into a message which is < 1500
                ClientMessage = ClientMessage.Slice(readBytes, (ClientMessage.Length - readBytes));

                //MEans more data coming for this message
                if (ClientMessage.Length == 0)
                {
                    return;
                }
            }

            ///0xFB/FA type Message type
            /////Eventually we would at a minimum remove this FA type check... This should be dynamically figured out on the server, if a message is expanding over multiple packets.
            if (MessageOpcodeType == MessageOpcodeTypes.ShortReliableMessage)
            {
                ///Pack Message here into MySession.SessionMessages
                ///Check message length first
                if (ClientMessage.Length > 255)
                {
                    MessageHeaderReliableLong thisMessageHeader = new(MessageOpcodeTypes.LongReliableMessage, (ushort)ClientMessage.Length, MySession.ServerMessageNumber);

                    Memory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 6];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..6]);
                    ClientMessage.CopyTo(WholeClientMessage[6..WholeClientMessage.Length]);

                    AddMessage(MySession, MySession.ServerMessageNumber, WholeClientMessage);
                }

                ///Message is < 255
                else
                {
                    MessageHeaderReliableShort thisMessageHeader = new(MessageOpcodeTypes.ShortReliableMessage, (byte)ClientMessage.Length, MySession.ServerMessageNumber);

                    Memory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 4];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..4]);
                    ClientMessage.CopyTo(WholeClientMessage[4..WholeClientMessage.Length]);

                    AddMessage(MySession, MySession.ServerMessageNumber, WholeClientMessage);
                }

                //Increment server message #
                MySession.IncrementServerMessageNumber();
            }

            ///0xFC Message type
            else if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
            {
                if (ClientMessage.Length > 255)
                {
                    MessageHeaderUnreliableLong thisMessageHeader = new(MessageOpcodeTypes.LongUnreliableMessage, (ushort)ClientMessage.Length);

                    Memory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 4];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..4]);
                    ClientMessage.CopyTo(WholeClientMessage[4..WholeClientMessage.Length]);

                    AddMessage(MySession, 0, WholeClientMessage);
                }

                ///Message is < 255
                else
                {
                    MessageHeaderUnreliableShort thisMessageHeader = new(MessageOpcodeTypes.ShortUnreliableMessage, (byte)ClientMessage.Length);

                    Memory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 2];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..2]);
                    ClientMessage.CopyTo(WholeClientMessage[2..WholeClientMessage.Length]);

                    AddMessage(MySession, 0, WholeClientMessage);
                }
            }
        }

        private void AddMessage(Session MySession, ushort MessageNum, Memory<byte> MyMessage)
        {
            MySession.sessionQueue.Add(new MessageStruct(MessageNum, MyMessage));
        }

    }
}
