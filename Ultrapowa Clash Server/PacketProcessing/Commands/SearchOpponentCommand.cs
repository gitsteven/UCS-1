using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Commande 700
    internal class SearchOpponentCommand : Command
    {
        #region Public Constructors

        public SearchOpponentCommand(BinaryReader br)
        {
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
        }

        #endregion Public Constructors

        //00 00 00 00 00 00 00 00 00 00 00 97

        #region Public Methods

        public override void Execute(Level level)
        {
            var l = ObjectManager.GetRandomOnlinePlayer();
            if (l != null)
            {
                l.Tick();
                var p = new EnemyHomeDataMessage(level.GetClient(), l, level);
                PacketManager.ProcessOutgoingPacket(p);
            }
        }

        #endregion Public Methods
    }
}