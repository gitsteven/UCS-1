namespace UCS.Logic
{
    internal class Achievement
    {
        #region Private Fields

        private const int m_vType = 0x015EF3C0;

        #endregion Private Fields

        #region Public Constructors

        public Achievement()
        {
        }

        public Achievement(int index)
        {
            //this.Name = ObjectManager.AchievementsData.GetData(index, 0).Name;
            Index = index;
            Unlocked = false;
            Value = 0;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id
        {
            get { return m_vType + Index; }
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public bool Unlocked { get; set; }
        public int Value { get; set; }

        #endregion Public Properties
    }
}