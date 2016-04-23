using Newtonsoft.Json.Linq;
using System;
using UCS.Core;
using UCS.GameFiles;

namespace UCS.Logic
{
    internal class CombatComponent : Component
    {
        #region Private Fields

        private const int m_vType = 0x01AB3F00;

        private int m_vAmmo;

        #endregion Private Fields

        #region Public Constructors

        public CombatComponent(ConstructionItem ci, Level level) : base(ci)
        {
            var bd = (BuildingData)ci.GetData();
            if (bd.AmmoCount != 0)
            {
                m_vAmmo = bd.AmmoCount;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public override int Type
        {
            get { return 1; }
        }

        #endregion Public Properties

        #region Public Methods

        public void FillAmmo()
        {
            var ca = GetParent().GetLevel().GetPlayerAvatar();
            var bd = (BuildingData)GetParent().GetData();
            var rd = ObjectManager.DataTables.GetResourceByName(bd.AmmoResource);

            if (ca.HasEnoughResources(rd, bd.AmmoCost))
            {
                ca.CommodityCountChangeHelper(0, rd, bd.AmmoCost);
                m_vAmmo = bd.AmmoCount;
            }
        }

        public override void Load(JObject jsonObject)
        {
            if (jsonObject["ammo"] != null)
            {
                m_vAmmo = jsonObject["ammo"].ToObject<int>();
            }
        }

        public override JObject Save(JObject jsonObject)
        {
            if (m_vAmmo != null)
            {
                jsonObject.Add("ammo", m_vAmmo);
            }
            return jsonObject;
        }

        #endregion Public Methods
    }
}