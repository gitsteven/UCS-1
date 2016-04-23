using System;
using System.Threading;

namespace UCS.Core.Threading
{
    internal class InterfaceThread
    {
        #region Private Properties

        /// <summary>
        /// Variable holding the thread itself
        /// </summary>
        private static Thread T { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Starts the Thread
        /// </summary>
        [STAThread]
        public static void Start()
        {
            T = new Thread(() => { });
            T.SetApartmentState(ApartmentState.STA); //Required running in single thread mode
            T.Start();
        }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }

        #endregion Public Methods
    }
}