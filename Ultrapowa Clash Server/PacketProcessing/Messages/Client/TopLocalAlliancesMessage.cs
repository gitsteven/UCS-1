using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopLocalAlliancesMessage : Message
    {
        #region Public Constructors

        public TopLocalAlliancesMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new LocalAlliancesMessage(Client));
        }

        #endregion Public Methods
    }
}