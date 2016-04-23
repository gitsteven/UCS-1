using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24104
    internal class OutOfSyncMessage : Message
    {
        #region Public Constructors

        public OutOfSyncMessage(Client client) : base(client)
        {
            SetMessageType(24104);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            Encrypt(data.ToArray());
        }

        #endregion Public Methods
    }
}