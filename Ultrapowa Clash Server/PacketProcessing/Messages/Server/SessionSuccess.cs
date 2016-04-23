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

using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 20100
    internal class SessionSuccess : Message
    {
        #region Public Fields

        public byte[] SessionKey;

        #endregion Public Fields

        #region Public Constructors

        public SessionSuccess(Client client, SessionRequest cka) : base(client)
        {
            SetMessageType(20100);
            SessionKey = Client.GenerateSessionKey();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddInt32(SessionKey.Length);
            pack.AddRange(SessionKey);
            SetData(pack.ToArray());
        }

        #endregion Public Methods
    }
}