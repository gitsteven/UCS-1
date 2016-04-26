using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Ultrapowa_Clash_Restarter
{
    class Program
    {
        static void Main()
        {
            // We wait 5 seconds, UCS must be fully closed
            Thread.Sleep(5000);
            // Foreach all files in directory for get the full path
            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                // Get file infos of the current processed $file
                FileVersionInfo p = FileVersionInfo.GetVersionInfo(file);
                if (p.ProductName == "Ultrapowa Clash Server")
                {
                    // If the product name of the file is equal to UCS, We start the file
                    Process.Start(file);
                    // We break the foreach
                    break;
                }
            }
        }
    }
}
