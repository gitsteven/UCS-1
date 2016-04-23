using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopPreviousGlobalPlayersMessage : Message
    {
        #region Public Constructors

        public TopPreviousGlobalPlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new PreviousGlobalPlayersMessage(Client));
        }

        #endregion Public Methods
    }
}