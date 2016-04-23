using UCS.Core.Threading;

namespace UCS
{
    internal class Program
    {
        #region Public Methods

        public static void Main(string[] args)
        {
            ConsoleThread.Start();
        }

        #endregion Public Methods
    }
}