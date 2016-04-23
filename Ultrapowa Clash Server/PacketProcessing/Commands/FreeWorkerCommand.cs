using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x209
    internal class FreeWorkerCommand : Command
    {
        #region Public Fields

        public int m_vTimeLeftSeconds;

        #endregion Public Fields

        #region Private Fields

        private readonly object m_vCommand;
        private readonly bool m_vIsCommandEmbedded;

        #endregion Private Fields

        #region Public Constructors

        public FreeWorkerCommand(BinaryReader br)
        {
            m_vTimeLeftSeconds = br.ReadInt32WithEndian();
            m_vIsCommandEmbedded = br.ReadBoolean();
            if (m_vIsCommandEmbedded)
            {
                Depth++;
                if (Depth >= MaxEmbeddedDepth)
                    throw new ArgumentException(
                        "A command contained embedded command depth was greater than max embedded commands.");
                m_vCommand = CommandFactory.Read(br);
            }
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute(Level level)
        {
            if (level.WorkerManager.GetFreeWorkers() == 0)
            {
                level.WorkerManager.FinishTaskOfOneWorker();
                if (m_vIsCommandEmbedded)
                {
                    Depth++;

                    if (Depth >= MaxEmbeddedDepth)
                        throw new ArgumentException(
                            "A command contained embedded command depth was greater than max embedded commands.");

                    ((Command)m_vCommand).Execute(level);
                }
            }
        }

        #endregion Public Methods
    }
}