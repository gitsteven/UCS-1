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

using System.IO;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class GetDeviceTokenMessage : Message
    {
        #region Public Fields

        public int Unknown1;
        public string UserToken;

        #endregion Public Fields

        #region Public Constructors

        public GetDeviceTokenMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                UserToken = br.ReadString();
                Unknown1 = br.ReadInt32();
            }
        }

        public override void Process(Level level)
        {
            var p = new SetDeviceTokenMessage(Client);
            p.UserToken = UserToken;
            if (UserToken != null || UserToken != string.Empty)
            {
                level.GetPlayerAvatar().SetToken(UserToken);
                DatabaseManager.Singelton.Save(level);
            }
            PacketManager.ProcessOutgoingPacket(p);
        }

        #endregion Public Methods
    }
}