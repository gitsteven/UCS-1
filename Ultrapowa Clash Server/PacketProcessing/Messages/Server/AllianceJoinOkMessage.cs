using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    //Packet 24303
    internal class AllianceJoinOkMessage : Message
    {
        #region Public Constructors

        public AllianceJoinOkMessage(Client client) : base(client)
        {
            SetMessageType(24303);
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