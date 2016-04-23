using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 605
    internal class PlaceHeroCommand : Command
    {
        #region Public Constructors

        public PlaceHeroCommand(BinaryReader br)
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