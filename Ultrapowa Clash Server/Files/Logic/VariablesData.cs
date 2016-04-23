namespace UCS.GameFiles
{
    internal class VariablesData : Data
    {
        #region Public Constructors

        public VariablesData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; set; }
        public int Value { get; set; }

        #endregion Public Properties
    }
}