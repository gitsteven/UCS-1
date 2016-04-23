namespace UCS.GameFiles
{
    internal class MissionsData : Data
    {
        #region Public Constructors

        public MissionsData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; set; }

        #endregion Public Properties
    }
}