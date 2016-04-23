using UCS.GameFiles;

namespace UCS.Logic
{
    internal class Trap : ConstructionItem
    {
        #region Public Constructors

        public Trap(Data data, Level l) : base(data, l)
        {
            AddComponent(new TriggerComponent());
        }

        #endregion Public Constructors

        #region Public Properties

        public override int ClassId
        {
            get { return 4; }
        }

        #endregion Public Properties

        #region Public Methods

        public TrapData GetTrapData()
        {
            return (TrapData)GetData();
        }

        #endregion Public Methods
    }
}