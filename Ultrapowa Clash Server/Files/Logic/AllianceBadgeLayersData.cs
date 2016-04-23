namespace UCS.GameFiles
{
    internal class AllianceBadgeLayersData : Data
    {
        #region Public Constructors

        public AllianceBadgeLayersData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string ExportName { get; set; }
        public string Name { get; set; }
        public int RequiredClanLevel { get; set; }
        public string SWF { get; set; }
        public string Type { get; set; }

        #endregion Public Properties
    }
}