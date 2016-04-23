using System;
using System.IO;
using System.Text;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class AttackResultMessage : Message
    {
        #region Public Constructors

        public AttackResultMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            Console.WriteLine("Packet Attack Result : " + Encoding.UTF8.GetString(GetData()));
        }

        public override void Process(Level level)
        {
        }

        #endregion Public Methods
    }
}