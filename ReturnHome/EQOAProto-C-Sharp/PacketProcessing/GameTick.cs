﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReturnHome.PacketProcessing
{
    class GameTick
    {
        private Stopwatch gameTimer;
        private int serverTick = 1000 / 10;
        private readonly RdpCommOut _rdpCommOut;
        private readonly SessionManager _sessionManager;
        private readonly WorldServer _worldServer;

        public GameTick(UDPListener udpListener, SessionManager sessionManager, WorldServer worldServer)
        {
            _sessionManager = sessionManager;
            _rdpCommOut = new(udpListener, _sessionManager);
            _worldServer = worldServer;
        }

        public async Task GameLoop()
        {
            gameTimer = new Stopwatch();

            while (true)
            {
                gameTimer.Restart();
                _worldServer.CreateObjectUpdates();
                ///Loop over Sessions and send updates if available
                _rdpCommOut.PrepPacket();

                gameTimer.Stop();

                if (gameTimer.ElapsedMilliseconds > serverTick)
                {
                    //Console.WriteLine("Server can't keep up");
                }

                await Task.Delay(Math.Max(0, serverTick - (int)gameTimer.ElapsedMilliseconds));
            }
        }

    }
}
