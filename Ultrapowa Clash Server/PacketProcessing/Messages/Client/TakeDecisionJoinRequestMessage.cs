using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class TakeDecisionJoinRequestMessage : Message
    {
        #region Public Constructors

        public TakeDecisionJoinRequestMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            // Console.WriteLine(Encoding.UTF8.GetString(GetData()));
        }

        public override void Process(Level level)
        {
            // PacketManager.ProcessOutgoingPacket(new DecisionJoinRequestMessage(Client));
        }

        #endregion Public Methods
    }
}