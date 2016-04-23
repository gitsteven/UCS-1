namespace UCS.GameFiles
{
    internal class ConstructionItemData : Data
    {
        #region Public Constructors

        public ConstructionItemData(CSVRow row, DataTable dt) : base(row, dt)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual int GetBuildCost(int level)
        {
            return -1;
        }

        public virtual ResourceData GetBuildResource(int level)
        {
            return null;
        }

        public virtual int GetConstructionTime(int level)
        {
            return -1;
        }

        public virtual int GetRequiredTownHallLevel(int level)
        {
            return -1;
        }

        public virtual int GetUpgradeLevelCount()
        {
            return -1;
        }

        public virtual bool IsTownHall()
        {
            return false;
        }

        #endregion Public Methods
    }
}