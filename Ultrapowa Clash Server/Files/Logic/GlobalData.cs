namespace UCS.GameFiles
{
    internal class GlobalData : Data
    {
        #region Public Constructors

        public GlobalData(CSVRow row, DataTable dt)
            : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string AltStringArray { get; set; }
        public bool BooleanValue { get; set; }
        public int NumberArray { get; set; }
        public int NumberValue { get; set; }
        public string StringArray { get; set; }
        public string TextValue { get; set; }

        #endregion Public Properties
    }
}