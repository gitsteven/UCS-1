using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x207
    internal class MissionProgressCommand : Command
    {
        #region Public Constructors

        public MissionProgressCommand(BinaryReader br)
        {
            /*
            MissionId = br.ReadUInt32WithEndian(); //missionId - 0x1406F40;
            Unknown1 = br.ReadUInt32WithEndian();
            */
        }

        #endregion Public Constructors

        #region Public Properties

        public uint MissionId { get; set; }
        public uint Unknown1 { get; set; }

        #endregion Public Properties
    }
}