/*
 * Program : Ultrapowa Clash Server
 * Description : A C# Writted 'Clash of Clans' Server Emulator !
 *
 * Authors:  Jean-Baptiste Martin <Ultrapowa at Ultrapowa.com>,
 *           And the Official Ultrapowa Developement Team
 *
 * Copyright (c) 2016  UltraPowa
 * All Rights Reserved.
 */

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UCS.PacketProcessing;

namespace UCS.Core.Network
{
    internal class Gateway
    {
        #region Private Fields

        private const int kHostConnectionBacklog = 30;
        private const int kPort = 9339;
        private IPAddress ip;

        #endregion Private Fields

        #region Public Properties

        public static Socket Socket { get; private set; }

        public IPAddress IP
        {
            get
            {
                if (ip == null)
                {
                    ip = (
                        from entry in Dns.GetHostEntry(Dns.GetHostName()).AddressList
                        where entry.AddressFamily == AddressFamily.InterNetwork
                        select entry
                        ).FirstOrDefault();
                }
                return ip;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public bool Host(int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Socket.Bind(new IPEndPoint(IPAddress.Any, port));
                Socket.Listen(kHostConnectionBacklog);
                Socket.BeginAccept(OnClientConnect, Socket);
            }
            catch (Exception e)
            {
                Console.WriteLine("[UCS]    Exception when attempting to host (" + port + "): " + e);
                Socket = null;
                return false;
            }
            return true;
        }

        public void Start()
        {
            if (Host(kPort))
                Console.WriteLine("[UCS]    Gateway started on port " + kPort);
        }

        #endregion Public Methods

        #region Private Methods

        private static void Disconnect()
        {
            Socket?.BeginDisconnect(false, OnEndHostComplete, Socket);
        }

        private static void OnEndHostComplete(IAsyncResult result)
        {
            Socket.Close(5);
            Socket = null;
        }

        private static void OnReceive(SocketRead read, byte[] data)
        {
            try
            {
                Client c = ResourcesManager.GetClient(read.Socket.Handle.ToInt64());
                c.DataStream.AddRange(data);
                Message p;
                while (c.TryGetPacket(out p))
                    PacketManager.ProcessIncomingPacket(p);
            }
            catch (Exception ex)
            {
                Debugger.WriteLine("[UCS]    Exception thrown when processing incoming packet : ", ex);
            }
        }

        private static void OnReceiveError(SocketRead read, Exception exception)
        {
            Debugger.WriteLine(
                "[UCS]    The client '" + ((IPEndPoint) read.Socket.RemoteEndPoint).Address + "' throw an exception",
                exception);
        }

        private void OnClientConnect(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = Socket.EndAccept(result);
                /* WebClient c = new WebClient();
                c.DownloadStringCompleted +=
                    (sender, e) =>
                    {
                        Console.WriteLine("[UCS]    Client connected (" +
                                          ((IPEndPoint) clientSocket.RemoteEndPoint).Address + ", " + e.Result.Trim() +
                                          ")");
                    };
                c.DownloadStringAsync(
                    new Uri("http://ipinfo.io/" + ((IPEndPoint) clientSocket.RemoteEndPoint).Address + "/country"));

                 */
                Console.WriteLine("[UCS]    Client connected (" + ((IPEndPoint) clientSocket.RemoteEndPoint).Address + ")");
                ResourcesManager.AddClient(new Client(clientSocket),
                    ((IPEndPoint) clientSocket.RemoteEndPoint).Address.ToString());
                SocketRead.Begin(clientSocket, OnReceive, OnReceiveError);
                Socket.BeginAccept(OnClientConnect, Socket);
            }
            catch (Exception e)
            {
                Debugger.WriteLine("[UCS]    Exception when accepting incoming connection", e);
            }
        }

        #endregion Private Methods
    }
}