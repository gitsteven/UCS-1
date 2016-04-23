namespace UCS.Logic
{
    internal class MovementComponent : Component
    {
        #region Private Fields

        private const int m_vType = 0x01AB3F00;

        #endregion Private Fields

        #region Public Properties

        public override int Type
        {
            get { return 4; }
        }

        #endregion Public Properties
    }
}