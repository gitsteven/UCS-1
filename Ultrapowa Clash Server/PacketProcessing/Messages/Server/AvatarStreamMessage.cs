using System.Collections.Generic;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24411
    internal class AvatarStreamMessage : Message
    {
        #region Private Fields

        private AvatarStreamEntry m_vAvatarStreamEntry;

        #endregion Private Fields

        #region Public Constructors

        public AvatarStreamMessage(Client client) : base(client)
        {
            SetMessageType(24411);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pl = Client.GetLevel().GetPlayerAvatar();
            var pack = new List<byte>();
            pack.AddInt32(2);
            pack.AddInt64(pl.GetId());
            pack.Add(0);
            pack.AddInt64(pl.GetAllianceId());
            pack.AddString(ObjectManager.GetAlliance(pl.GetAllianceId()).GetAllianceName());
            pack.AddInt32(0);
            pack.AddInt32(0);
            pack.AddInt32(0);
            pack.Add(0);
            pack.AddString("Win");
            pack.Add(0);
            pack.AddInt32(0);
            pack.AddInt32(0);
            pack.AddInt32(0);
            Encrypt(pack.ToArray());
        }

        #endregion Public Methods
    }
}