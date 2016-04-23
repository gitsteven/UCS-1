using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24312
    internal class AllianceStreamEntryMessage : Message
    {
        #region Private Fields

        private StreamEntry m_vStreamEntry;

        #endregion Private Fields

        #region Public Constructors

        public AllianceStreamEntryMessage(Client client) : base(client)
        {
            SetMessageType(24312);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddRange(m_vStreamEntry.Encode());
            Encrypt(pack.ToArray());
        }

        public void SetStreamEntry(StreamEntry entry)
        {
            m_vStreamEntry = entry;
        }

        #endregion Public Methods
    }
}