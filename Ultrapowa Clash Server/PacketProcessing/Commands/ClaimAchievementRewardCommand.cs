using System.IO;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x20B
    internal class ClaimAchievementRewardCommand : Command
    {
        #region Public Constructors

        public ClaimAchievementRewardCommand(BinaryReader br)
        {
            AchievementId = br.ReadInt32WithEndian(); //= achievementId - 0x015EF3C0;
            Unknown1 = br.ReadUInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Properties

        public int AchievementId { get; set; }
        public uint Unknown1 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute(Level level)
        {
            var ca = level.GetPlayerAvatar();

            var ad = (AchievementData)ObjectManager.DataTables.GetDataById(AchievementId);

            ca.AddDiamonds(ad.DiamondReward);
            ca.AddExperience(ad.ExpReward);
            ca.SetAchievment(ad, true);
        }

        #endregion Public Methods
    }
}