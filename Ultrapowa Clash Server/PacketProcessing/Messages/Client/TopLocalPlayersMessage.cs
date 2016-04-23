using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopLocalPlayersMessage : Message
    {
        #region Public Constructors

        public TopLocalPlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new LocalPlayersMessage(Client));
        }

        #endregion Public Methods
    }
}