using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AskForAllianceWarHistoryMessage : Message
    {
        #region Public Constructors

        public AskForAllianceWarHistoryMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Private Properties

        private static long AllianceID { get; set; }
        private static long WarID { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                AllianceID = br.ReadInt64();
                WarID = br.ReadInt64();
            }
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new AllianceWarHistoryMessage(Client));
        }

        #endregion Public Methods
    }
}