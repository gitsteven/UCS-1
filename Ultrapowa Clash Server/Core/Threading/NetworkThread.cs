using System;
using System.Configuration;
using System.Threading;
using UCS.Network;

namespace UCS.Core.Threading
{
    internal class NetworkThread
    {
        #region Public Fields

        public static string Author = "ExPl0itR";

        public static string Description = "Includes the Core (PacketManager etc.)";

        public static string Name = "Network Thread";

        public static string Version = "1.0.0";

        #endregion Public Fields

        #region Private Properties

        private static Thread T { get; set; }

        #endregion Private Properties

        #region Public Methods

        public static void Start()
        {
            T = new Thread(() =>
            {
                new PacketManager().Start();
                new MessageManager().Start();
                new ResourcesManager();
                new ObjectManager();
                new Gateway().Start();
                //HTTP API = new HTTP(Convert.ToInt32(ConfigurationManager.AppSettings["DebugPort"]));
                //new UCSList();
                Console.WriteLine("[UCS]    Server started, let's play Clash of Clans!");
            });
            T.Start();
        }

        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }

        #endregion Public Methods
    }
}