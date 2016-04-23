using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class ShutdownServerGameOpCommand : GameOpCommand
    {
        #region Private Fields

        private string[] m_vArgs;

        #endregion Private Fields

        #region Public Constructors

        public ShutdownServerGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(4);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                {
                    var p = new ShutdownStartedMessage(onlinePlayer.GetClient());
                    p.SetCode(5);
                    PacketManager.ProcessOutgoingPacket(p);
                }
            }
            else
            {
                SendCommandFailedMessage(level.GetClient());
            }
        }

        #endregion Public Methods
    }
}