using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24111
    internal class AvailableServerCommandMessage : Message
    {
        #region Private Fields

        private Command m_vCommand;
        private int m_vServerCommandId;

        #endregion Private Fields

        #region Public Constructors

        public AvailableServerCommandMessage(Client client) : base(client)
        {
            SetMessageType(24111);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddInt32(m_vServerCommandId);
            pack.AddRange(m_vCommand.Encode());
            Encrypt(pack.ToArray());
        }

        public void SetCommand(Command c)
        {
            m_vCommand = c;
        }

        public void SetCommandId(int id)
        {
            m_vServerCommandId = id;
        }

        #endregion Public Methods
    }
}