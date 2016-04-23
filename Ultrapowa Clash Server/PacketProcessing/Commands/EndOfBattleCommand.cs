using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class EndOfBattleCommand : Command
    {
        #region Public Constructors

        public EndOfBattleCommand(BinaryReader br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
        }

        #endregion Public Methods
    }
}