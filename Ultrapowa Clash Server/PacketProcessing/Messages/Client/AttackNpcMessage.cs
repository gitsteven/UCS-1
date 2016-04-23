using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AttackNpcMessage : Message
    {
        #region Public Constructors

        public AttackNpcMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public int LevelId { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                LevelId = br.ReadInt32WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var san = new NpcDataMessage(Client, level, this);
            PacketManager.ProcessOutgoingPacket(san);
        }

        #endregion Public Methods
    }
}