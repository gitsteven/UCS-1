using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24111
    internal class AvatarNameChangeOkMessage : Message
    {
        #region Private Fields

        private readonly int m_vServerCommandType;
        private string m_vAvatarName;

        #endregion Private Fields

        #region Public Constructors

        public AvatarNameChangeOkMessage(Client client) : base(client)
        {
            SetMessageType(24111);

            m_vServerCommandType = 0x03;
            m_vAvatarName = "Megapumba";
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();

            pack.AddInt32(m_vServerCommandType);
            pack.AddString(m_vAvatarName);
            pack.AddInt32(1);
            pack.AddInt32(-1);

            Encrypt(pack.ToArray());
        }

        public string GetAvatarName()
        {
            return m_vAvatarName;
        }

        public void SetAvatarName(string name)

        {
            m_vAvatarName = name;
        }

        #endregion Public Methods
    }
}