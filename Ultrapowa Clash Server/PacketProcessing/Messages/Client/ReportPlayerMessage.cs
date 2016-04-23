using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class ReportPlayerMessage : Message
    {
        #region Public Constructors

        public ReportPlayerMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
            }
        }

        public override void Process(Level level)
        {
        }

        #endregion Public Methods
    }
}