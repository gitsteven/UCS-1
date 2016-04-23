using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 10108
    internal class KeepAliveMessage : Message
    {
        #region Public Constructors

        public KeepAliveMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new KeepAliveOkMessage(Client, this));
        }

        #endregion Public Methods
    }
}