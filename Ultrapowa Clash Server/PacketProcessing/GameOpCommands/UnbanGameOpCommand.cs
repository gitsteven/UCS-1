using System;
using UCS.Core;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class UnbanGameOpCommand : GameOpCommand
    {
        #region Private Fields

        private readonly string[] m_vArgs;

        #endregion Private Fields

        #region Public Constructors

        public UnbanGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(2);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                if (m_vArgs.Length >= 2)
                {
                    try
                    {
                        var id = Convert.ToInt64(m_vArgs[1]);
                        var l = ResourcesManager.GetPlayer(id);
                        if (l != null)
                        {
                            l.SetAccountStatus(0);
                        }
                        else
                        {
                            Debugger.WriteLine("Unban failed: id " + id + " not found");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debugger.WriteLine("Unban failed with error: " + ex);
                    }
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