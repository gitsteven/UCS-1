namespace UCS.GameFiles
{
    internal class AlliancePortalData : Data
    {
        #region Public Constructors

        public AlliancePortalData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string ExportName { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string SWF { get; set; }
        public string TID { get; set; }
        public int Width { get; set; }

        #endregion Public Properties
    }
}