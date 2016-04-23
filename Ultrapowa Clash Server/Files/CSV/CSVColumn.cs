using System.Collections.Generic;

namespace UCS.GameFiles
{
    internal class CSVColumn
    {
        #region Private Fields

        private readonly List<string> m_vValues;

        #endregion Private Fields

        #region Public Constructors

        public CSVColumn()
        {
            m_vValues = new List<string>();
        }

        #endregion Public Constructors

        #region Public Methods

        public static int GetArraySize(int currentOffset, int nextOffset)
        {
            return nextOffset - currentOffset;
        }

        public void Add(string value)
        {
            //if (value == string.Empty)
            //    m_vValues.Add(m_vValues.Last());
            //else
            m_vValues.Add(value);
        }

        public string Get(int row)
        {
            return m_vValues[row];
        }

        public int GetSize()
        {
            return m_vValues.Count;
        }

        #endregion Public Methods
    }
}