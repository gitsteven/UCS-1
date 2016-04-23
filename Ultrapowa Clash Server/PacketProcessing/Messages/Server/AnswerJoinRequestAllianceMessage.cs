using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class AnswerJoinRequestAllianceMessage : Message
    {
        #region Private Fields

        private readonly int m_vServerCommandType;
        private string m_vAvatarName;

        #endregion Private Fields

        #region Public Constructors

        public AnswerJoinRequestAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24317);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();

            Encrypt(pack.ToArray());
        }

        #endregion Public Methods
    }
}