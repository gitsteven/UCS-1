using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class BookmarkAddAllianceMessage : Message
    {
        #region Public Constructors

        public BookmarkAddAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24343);
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