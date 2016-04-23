namespace UCS.GameFiles
{
    internal class WarData : Data
    {
        #region Public Constructors

        public WarData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public bool DisableProduction { get; set; }
        public string Name { get; set; }
        public int PreparationMinutes { get; set; }
        public int TeamSize { get; set; }
        public int WarMinutes { get; set; }

        #endregion Public Properties
    }
}