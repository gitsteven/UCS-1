using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class ChangeAvatarNameMessage : Message
    {
        #region Public Constructors

        public ChangeAvatarNameMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public string PlayerName { get; set; }

        public int PlayerNameLength { get; set; }

        public byte Unknown1 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                PlayerName = br.ReadScString();
                Unknown1 = br.ReadByte();
            }
        }

        public override void Process(Level level)
        {
            level.GetPlayerAvatar().SetName(PlayerName);
            var p = new AvatarNameChangeOkMessage(Client);
            p.SetAvatarName(level.GetPlayerAvatar().GetAvatarName());
            PacketManager.ProcessOutgoingPacket(p);
        }

        #endregion Public Methods
    }
}