namespace UCS.GameFiles
{
    internal class EffectsData : Data
    {
        #region Public Constructors

        public EffectsData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; set; }

        #endregion Public Properties
    }
}