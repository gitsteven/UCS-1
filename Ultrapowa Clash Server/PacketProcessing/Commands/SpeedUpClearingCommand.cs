using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x202
    internal class SpeedUpClearingCommand : Command
    {
        #region Private Fields

        private readonly int m_vObstacleId;

        #endregion Private Fields

        #region Public Constructors

        public SpeedUpClearingCommand(BinaryReader br)
        {
            m_vObstacleId = br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
            var go = level.GameObjectManager.GetGameObjectByID(m_vObstacleId);
            if (go != null)
            {
                if (go.ClassId == 3)
                    ((Obstacle)go).SpeedUpClearing();
            }
        }

        #endregion Public Methods
    }
}