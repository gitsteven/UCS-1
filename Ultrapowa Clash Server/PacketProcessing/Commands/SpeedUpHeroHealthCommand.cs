using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x0212
    internal class SpeedUpHeroHealthCommand : Command
    {
        #region Private Fields

        private int m_vBuildingId;

        #endregion Private Fields

        #region Public Constructors

        public SpeedUpHeroHealthCommand(BinaryReader br)
        {
            /*
            m_vBuildingId = br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
            */
        }

        #endregion Public Constructors
    }
}