using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 20113
    internal class SetDeviceTokenMessage : Message
    {
        #region Public Constructors

        public SetDeviceTokenMessage(Client client) : base(client)
        {
            SetMessageType(20113);
        }

        #endregion Public Constructors

        #region Public Properties

        public string UserToken { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddString(UserToken);
            Encrypt(pack.ToArray());
        }

        #endregion Public Methods
    }
}