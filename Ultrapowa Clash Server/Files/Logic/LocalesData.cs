namespace UCS.GameFiles
{
    internal class LocalesData : Data
    {
        #region Public Constructors

        public LocalesData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Description { get; set; }
        public bool HasEvenSpaceCharacters { get; set; }
        public string HelpshiftSDKLanguage { get; set; }
        public string HelpshiftSDKLanguageAndroid { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string TestExcludes { get; set; }
        public bool TestLanguage { get; set; }
        public string UsedSystemFont { get; set; }

        #endregion Public Properties
    }
}