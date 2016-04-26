using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UCSClientPatcher
{
    internal class Patcher
    {
        internal static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        internal static string AssemblyVersion
        {
            get
            {
                return "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        internal static void Main(string[] args)
        {
            string fileName = args[0].ToString();
            byte[] fileBytes = File.ReadAllBytes(fileName);
            byte[] searchPattern = HexToByteArray("9b39b440ff6c13ad07b506fc55e37f69856895c3fd5ab35978cdf5e34eb37471");
            byte[] replacePattern = HexToByteArray("72f1a4a4c48e44da0c42310f800e96624e6dc6a641a9d41c3b5039d8dfadc27e");

            Console.Title = "Ultrapowa Client Patcher " + AssemblyVersion + " - © 2016";
            Console.WriteLine(
                @"
    888     888 888    88888888888 8888888b.         d8888 8888888b.   .d88888b.  888       888        d8888
    888     888 888        888     888   Y88b       d88888 888   Y88b d88P' 'Y88b 888   o   888       d88888
    888     888 888        888     888    888      d88P888 888    888 888     888 888  d8b  888      d88P888
    888     888 888        888     888   d88P     d88P 888 888   d88P 888     888 888 d888b 888     d88P 888
    888     888 888        888     8888888P'     d88P  888 8888888P'  888     888 888d88888b888    d88P  888
    888     888 888        888     888 T88b     d88P   888 888        888     888 88888P Y88888   d88P   888
    Y88b. .d88P 888        888     888  T88b   d8888888888 888        Y88b. .d88P 8888P   Y8888  d8888888888
     'Y88888P'  88888888   888     888   T88b d88P     888 888         'Y88888P'  888P     Y888 d88P     888
                  ");
                
            Console.WriteLine("[UCS]    -> This program is by the Ultrapowa Network development team.");
            Console.WriteLine("[UCS]    -> You can find the source at www.ultrapowa.com and https://github.com/UltraPowaDev/UCS/");
            Console.WriteLine("[UCS]    -> This program can be used with args - Ultrapowa Client Patcher.exe {Binary Name}");

            //Search
            IEnumerable<int> positions = FindPattern(fileBytes, searchPattern);
            if (positions.Count() == 0)
            {
                Console.WriteLine("[UCS] Pattern not found.");
                Console.Read();
                return;
            }

            //Backup
            string backupFileName = fileName + ".bak";
            File.Copy(fileName, backupFileName);
            Console.WriteLine("[UCS] Backup file: {0} -> {1}", fileName, backupFileName);

            foreach (int pos in positions)
            {
                //Replace
                Console.WriteLine("[UCS] Key offset: 0x{0}", pos.ToString("X8"));
                using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Open, FileAccess.Write)))
                {
                    bw.BaseStream.Seek(pos, SeekOrigin.Begin);
                    bw.Write(replacePattern);
                }
                Console.WriteLine("[UCS] File: {0} patched", fileName);
            }
            Console.Read();
        }

        public static IEnumerable<int> FindPattern(byte[] fileBytes, byte[] searchPattern)
        {
            if ((searchPattern != null) && (fileBytes.Length >= searchPattern.Length))
                for (int i = 0; i < fileBytes.Length - searchPattern.Length + 1; i++)
                    if (!searchPattern.Where((data, index) => !fileBytes[i + index].Equals(data)).Any())
                        yield return i;
        }
    }
}