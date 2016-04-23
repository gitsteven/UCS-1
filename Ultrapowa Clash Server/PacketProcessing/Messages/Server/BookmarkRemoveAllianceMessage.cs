using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class BookmarkRemoveAllianceMessage : Message
    {
        #region Public Constructors

        public BookmarkRemoveAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24344);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            Encrypt(data.ToArray());
        }

        #endregion Public Methods
    }
}