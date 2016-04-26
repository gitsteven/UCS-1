﻿/*
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
        IPAddress IPAddress;

        public static Socket Socket { get; set; }

        public IPAddress IP => IPAddress ?? (IPAddress = (
            from entry in Dns.GetHostEntry(Dns.GetHostName()).AddressList
            where entry.AddressFamily == AddressFamily.InterNetwork
            select entry
            ).FirstOrDefault());

        public void Start()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Socket.Bind(new IPEndPoint(IPAddress.Any, 9339));
                Socket.Listen(100);
                Socket.BeginAccept(OnClientConnect, Socket);
                Console.WriteLine("[UCS]    Gateway started on port 9339");
            }
            catch (Exception e)
            {
                Console.WriteLine("[UCS]    Exception when attempting to host the server : " + e);
                Socket = null;
            }
        }

        static void OnReceive(SocketRead read, byte[] data)
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

        static void OnReceiveError(SocketRead read, Exception exception)
        {
            Debugger.WriteLine("[UCS]    The client '" + ((IPEndPoint) read.Socket.RemoteEndPoint).Address + "' throw an exception", exception);
        }

        void OnClientConnect(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = Socket.EndAccept(result);
                Console.WriteLine("[UCS]    Client connected (" + ((IPEndPoint) clientSocket.RemoteEndPoint).Address + ")");
                ResourcesManager.AddClient(new Client(clientSocket), ((IPEndPoint) clientSocket.RemoteEndPoint).Address.ToString());
                SocketRead.Begin(clientSocket, OnReceive, OnReceiveError);
                Socket.BeginAccept(OnClientConnect, Socket);
            }
            catch (Exception e)
            {
                Debugger.WriteLine("[UCS]    Exception when accepting incoming connection", e);
            }
        }
    }

    public class SocketRead
    {
        SocketRead(Socket socket, IncomingReadHandler readHandler, IncomingReadErrorHandler errorHandler = null)
        {
            Socket = socket;
            _readHandler = readHandler;
            _errorHandler = errorHandler;
            BeginReceive();
        }

        public Socket Socket { get; }

        public static SocketRead Begin(Socket socket, IncomingReadHandler readHandler, IncomingReadErrorHandler errorHandler = null)
        {
            return new SocketRead(socket, readHandler, errorHandler);
        }

        readonly byte[] _buffer = new byte[1024];

        readonly IncomingReadErrorHandler _errorHandler;

        readonly IncomingReadHandler _readHandler;

        public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

        public delegate void IncomingReadHandler(SocketRead read, byte[] data);

        void BeginReceive()
        {
            Socket.BeginReceive(_buffer, 0, 1024, SocketFlags.None, OnReceive, this);
        }

        void OnReceive(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    var bytesRead = Socket.EndReceive(result);
                    if (bytesRead > 0)
                    {
                        var read = new byte[bytesRead];
                        Array.Copy(_buffer, 0, read, 0, bytesRead);

                        _readHandler(this, read);
                        Begin(Socket, _readHandler, _errorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                _errorHandler?.Invoke(this, e);
            }
        }
    }
}