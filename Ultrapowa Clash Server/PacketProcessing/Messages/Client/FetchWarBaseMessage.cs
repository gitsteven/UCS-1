using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 15000

    internal class FetchWarBaseMessage : Message
    {
        #region Public Constructors

        public FetchWarBaseMessage(Client client, BinaryReader br) : base(client, br)
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