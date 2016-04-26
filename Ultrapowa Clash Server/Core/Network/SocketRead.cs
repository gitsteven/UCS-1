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
using System.Net.Sockets;

//using System.Collections.Generic;

namespace UCS.Core.Network
{
    public class SocketRead
    {
        #region Public Fields

        public const int kBufferSize = 256;

        #endregion Public Fields

        #region Private Constructors

        private SocketRead(Socket socket, IncomingReadHandler readHandler, IncomingReadErrorHandler errorHandler = null)
        {
            Socket = socket;
            this.readHandler = readHandler;
            this.errorHandler = errorHandler;
            BeginReceive();
        }

        #endregion Private Constructors

        #region Public Properties

        public Socket Socket { get; }

        #endregion Public Properties

        #region Public Methods

        public static SocketRead Begin(Socket socket, IncomingReadHandler readHandler,
            IncomingReadErrorHandler errorHandler = null)
        {
            return new SocketRead(socket, readHandler, errorHandler);
        }

        #endregion Public Methods

        #region Private Fields

        private readonly byte[] buffer = new byte[kBufferSize];

        private readonly IncomingReadErrorHandler errorHandler;

        private readonly IncomingReadHandler readHandler;

        #endregion Private Fields

        #region Public Delegates

        public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

        public delegate void IncomingReadHandler(SocketRead read, byte[] data);

        #endregion Public Delegates

        #region Private Methods

        private void BeginReceive()
        {
            Socket.BeginReceive(buffer, 0, kBufferSize, SocketFlags.None, OnReceive, this);
        }

        private void OnReceive(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    var bytesRead = Socket.EndReceive(result);
                    if (bytesRead > 0)
                    {
                        var read = new byte[bytesRead];
                        Array.Copy(buffer, 0, read, 0, bytesRead);

                        readHandler(this, read);
                        Begin(Socket, readHandler, errorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                if (errorHandler != null)
                    errorHandler(this, e);
            }
        }

        #endregion Private Methods
    }
}