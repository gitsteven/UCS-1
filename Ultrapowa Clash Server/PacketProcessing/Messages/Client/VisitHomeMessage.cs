using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14113
    internal class VisitHomeMessage : Message
    {
        #region Public Constructors

        public VisitHomeMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public long AvatarId { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                AvatarId = br.ReadInt64WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var targetLevel = ResourcesManager.GetPlayer(AvatarId);
            targetLevel.Tick();
            //Clan clan;
            PacketManager.ProcessOutgoingPacket(new VisitedHomeDataMessage(Client, targetLevel, level));
            //if (clan != null)*/
            //    PacketHandler.ProcessOutgoingPacket(new ServerAllianceChatHistory(this.Client, clan));
        }

        #endregion Public Methods
    }
}