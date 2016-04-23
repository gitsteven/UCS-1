namespace UCS.Logic
{
    internal class BunkerComponent : Component
    {
        #region Private Fields

        private const int m_vType = 0x01AB3F00;

        #endregion Private Fields

        #region Public Properties

        public override int Type
        {
            get { return 7; }
        }

        #endregion Public Properties
    }
}