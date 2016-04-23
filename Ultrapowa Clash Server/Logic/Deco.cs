using Newtonsoft.Json.Linq;
using UCS.GameFiles;

namespace UCS.Logic
{
    internal class Deco : GameObject
    {
        #region Private Fields

        private Level m_vLevel;

        #endregion Private Fields

        #region Public Constructors

        public Deco(Data data, Level l) : base(data, l)
        {
            m_vLevel = l;
        }

        #endregion Public Constructors

        #region Public Properties

        public override int ClassId
        {
            get { return 6; }
        }

        #endregion Public Properties

        #region Public Methods

        public DecoData GetDecoData()
        {
            return (DecoData)GetData();
        }

        public new void Load(JObject jsonObject)
        {
            base.Load(jsonObject);
        }

        public new JObject Save(JObject jsonObject)
        {
            base.Save(jsonObject);
            return jsonObject;
        }

        #endregion Public Methods
    }
}