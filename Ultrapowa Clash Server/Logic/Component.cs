/*
 * Program : Ultrapowa Clash Server
 * Description : A C# Writted 'Clash of Clans' Server Emulator !
 *
 * Authors:  Jean-Baptiste Martin <Ultrapowa at Ultrapowa.com>,
 *           And the Official Ultrapowa Developement Team
 *
 * Copyright (c) 2016  UltraPowa
 * All Rights Reserved.
 */

using Newtonsoft.Json.Linq;

namespace UCS.Logic
{
    internal class Component
    {
        #region Public Properties

        public virtual int Type
        {
            get { return -1; }
        }

        #endregion Public Properties

        #region Private Fields

        readonly GameObject m_vParentGameObject;
        bool m_vIsEnabled;

        #endregion Private Fields

        //a1 + 8

        #region Public Constructors

        public Component()
        {
            //do not modify
        }

        public Component(GameObject go)
        {
            m_vIsEnabled = true;
            m_vParentGameObject = go;
        }

        #endregion Public Constructors

        #region Public Methods

        public GameObject GetParent()
        {
            return m_vParentGameObject;
        }

        public bool IsEnabled()
        {
            return m_vIsEnabled;
        }

        public virtual void Load(JObject jsonObject)
        {
        }

        public virtual JObject Save(JObject jsonObject)
        {
            return jsonObject;
        }

        public void SetEnabled(bool status)
        {
            m_vIsEnabled = status;
        }

        public virtual void Tick()
        {
        }

        #endregion Public Methods
    }
}