using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class BookmarkMessage : Message
    {
        #region Public Constructors

        public BookmarkMessage(Client client) : base(client)
        {
            SetMessageType(24340);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt64(1);
            data.AddInt64(2);
            Encrypt(data.ToArray());
        }

        #endregion Public Methods
    }
}