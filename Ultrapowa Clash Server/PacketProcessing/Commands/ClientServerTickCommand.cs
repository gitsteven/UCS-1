using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class ClientServerTickCommand : Command
    {
        #region Public Constructors

        public ClientServerTickCommand(BinaryReader br)
        {
            Unknown1 = br.ReadInt32();
            Tick = br.ReadInt32();
        }

        #endregion Public Constructors

        #region Public Properties

        public static int Tick { get; set; }
        public static int Unknown1 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute(Level level)
        {
        }

        #endregion Public Methods
    }
}