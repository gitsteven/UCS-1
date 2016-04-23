using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x1F8
    internal class SpeedUpConstructionCommand : Command
    {
        #region Private Fields

        private readonly int m_vBuildingId;

        #endregion Private Fields

        #region Public Constructors

        public SpeedUpConstructionCommand(BinaryReader br)
        {
            m_vBuildingId = br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
            var go = level.GameObjectManager.GetGameObjectByID(m_vBuildingId);
            if (go != null)
            {
                if (go.ClassId == 0 || go.ClassId == 4)
                {
                    ((ConstructionItem)go).SpeedUpConstruction();
                }
            }
        }

        #endregion Public Methods
    }
}