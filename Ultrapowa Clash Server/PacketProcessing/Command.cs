using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class Command
    {
        #region Public Fields

        public const int MaxEmbeddedDepth = 10;

        #endregion Public Fields

        #region Internal Properties

        internal int Depth { get; set; }

        #endregion Internal Properties

        #region Public Methods

        public virtual byte[] Encode()
        {
            return new List<byte>().ToArray();
        }

        public virtual void Execute(Level level)
        {
        }

        #endregion Public Methods
    }
}