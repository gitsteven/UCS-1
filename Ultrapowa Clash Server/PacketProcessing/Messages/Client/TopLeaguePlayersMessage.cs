using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopLeaguePlayersMessage : Message
    {
        #region Public Constructors

        public TopLeaguePlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new LeaguePlayersMessage(Client));
        }

        #endregion Public Methods
    }
}