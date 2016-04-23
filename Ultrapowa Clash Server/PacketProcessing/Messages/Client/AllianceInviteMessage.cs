using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class AllianceInviteMessage : Message
    {
        #region Public Constructors

        public AllianceInviteMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
        }

        #endregion Public Methods
    }
}