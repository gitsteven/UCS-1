using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x20A
    internal class BuyShieldCommand : Command
    {
        #region Public Constructors

        public BuyShieldCommand(BinaryReader br)
        {
            ShieldId = br.ReadInt32WithEndian(); //= shieldId - 0x01312D00;
            Unknown1 = br.ReadUInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Properties

        public int ShieldId { get; set; }
        public uint Unknown1 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute(Level level)
        {
            Console.WriteLine(ShieldId);
            Console.WriteLine(Unknown1);
        }

        #endregion Public Methods
    }
}