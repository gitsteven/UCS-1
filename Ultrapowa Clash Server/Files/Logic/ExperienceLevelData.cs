namespace UCS.GameFiles
{
    internal class ExperienceLevelData : Data
    {
        #region Public Constructors

        public ExperienceLevelData(CSVRow row, DataTable dt)
            : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public int ExpPoints { get; set; }

        #endregion Public Properties
    }
}