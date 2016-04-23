using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 20161
    internal class ShutdownStartedMessage : Message
    {
        #region Private Fields

        private int m_vCode;

        #endregion Private Fields

        #region Public Constructors

        public ShutdownStartedMessage(Client client)
            : base(client)
        {
            SetMessageType(20161);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt32(m_vCode);
            Encrypt(data.ToArray());
        }

        public void SetCode(int code)
        {
            m_vCode = code;
        }

        #endregion Public Methods
    }
}