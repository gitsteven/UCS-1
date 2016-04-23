using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24115
    internal class ServerErrorMessage : Message
    {
        #region Private Fields

        private string m_vErrorMessage;

        #endregion Private Fields

        #region Public Constructors

        public ServerErrorMessage(Client client)
            : base(client)
        {
            SetMessageType(24115);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddString(m_vErrorMessage);
            Encrypt(data.ToArray());
        }

        public void SetErrorMessage(string message)
        {
            m_vErrorMessage = message;
        }

        #endregion Public Methods
    }
}