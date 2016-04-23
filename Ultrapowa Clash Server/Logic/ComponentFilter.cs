namespace UCS.Logic
{
    internal class ComponentFilter : GameObjectFilter
    {
        #region Public Fields

        public int Type;

        #endregion Public Fields

        //a1 + 20

        #region Public Constructors

        public ComponentFilter(int type)
        {
            Type = type;
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool IsComponentFilter()
        {
            return true;
        }

        public bool TestComponent(Component c)
        {
            var go = c.GetParent();
            return TestGameObject(go);
        }

        public new bool TestGameObject(GameObject go)
        {
            var result = false;
            var c = go.GetComponent(Type, true);
            if (c != null)
                result = base.TestGameObject(go);
            return result;
        }

        #endregion Public Methods
    }
}