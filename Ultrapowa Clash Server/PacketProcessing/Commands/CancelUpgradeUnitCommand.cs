using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x203
    internal class CancelUpgradeUnitCommand : Command
    {
        #region Public Constructors

        public CancelUpgradeUnitCommand(BinaryReader br)
        {
            /*
            BuildingId = br.ReadUInt32WithEndian(); //buildingId - 0x1DCD6500;
            Unknown1 = br.ReadUInt32WithEndian();
            */
        }

        #endregion Public Constructors

        #region Public Properties

        public uint BuildingId { get; set; }
        public uint Unknown1 { get; set; }

        #endregion Public Properties
    }
}