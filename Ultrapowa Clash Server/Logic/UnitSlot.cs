using System.IO;
using UCS.GameFiles;
using UCS.Helpers;

namespace UCS.Logic
{
    internal class UnitSlot
    {
        #region Public Fields

        public int Count;
        public int Level;
        public CombatItemData UnitData;

        #endregion Public Fields

        //a1 + 4
        //a1 + 8
        //a1 + 12

        #region Public Constructors

        public UnitSlot(CombatItemData cd, int level, int count)
        {
            UnitData = cd;
            Level = level;
            Count = count;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Decode(BinaryReader br)
        {
            UnitData = (CombatItemData)br.ReadDataReference();
            Level = br.ReadInt32WithEndian();
            Count = br.ReadInt32WithEndian();
        }

        #endregion Public Methods
    }
}