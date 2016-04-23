namespace UCS.GameFiles
{
    internal class ShieldData : Data
    {
        #region Public Constructors

        public ShieldData(CSVRow row, DataTable dt)
            : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public int CooldownS { get; set; }
        public int Diamonds { get; set; }
        public string IconExportName { get; set; }
        public string IconSWF { get; set; }
        public string InfoTID { get; set; }
        public int LockedAboveScore { get; set; }
        public string TID { get; set; }
        public int TimeH { get; set; }

        #endregion Public Properties
    }
}