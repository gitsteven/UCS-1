using System.Collections.Generic;

namespace UCS.Logic
{
    internal class GameObjectFilter
    {
        #region Private Fields

        //a1 + 4 gameobjecttypes
        //a1 + 8
        //a1 + 12
        private List<int> m_vIgnoredObjects;

        #endregion Private Fields

        //a1 + 16

        #region Public Methods

        public void AddIgnoreObject(GameObject go)
        {
            if (m_vIgnoredObjects == null)
                m_vIgnoredObjects = new List<int>();
            m_vIgnoredObjects.Add(go.GlobalId);
        }

        public virtual bool IsComponentFilter()
        {
            return false;
        }

        public void RemoveAllIgnoreObjects()
        {
            if (m_vIgnoredObjects != null)
            {
                m_vIgnoredObjects.Clear();
                m_vIgnoredObjects = null;
            }
        }

        public bool TestGameObject(GameObject go)
        {
            var result = true;
            if (m_vIgnoredObjects != null)
            {
                result = m_vIgnoredObjects.IndexOf(go.GlobalId) == -1;
            }
            return result;
        }

        #endregion Public Methods
    }
}