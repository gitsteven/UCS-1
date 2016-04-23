namespace UCS.GameFiles
{
    internal class AllianceBadgesData : Data
    {
        #region Public Constructors

        public AllianceBadgesData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string IconExportName { get; set; }
        public string IconLayer0 { get; set; }
        public string IconLayer1 { get; set; }
        public string IconLayer2 { get; set; }
        public string IconSWF { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }
}