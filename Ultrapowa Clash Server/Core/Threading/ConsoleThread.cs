/*
 * Program : Ultrapowa Clash Server
 * Description : A C# Writted 'Clash of Clans' Server Emulator !
 *
 * Authors:  Jean-Baptiste Martin <Ultrapowa at Ultrapowa.com>,
 *           And the Official Ultrapowa Developement Team
 *
 * Copyright (c) 2016  UltraPowa
 * All Rights Reserved.
 */

using System;
using System.Configuration;
using System.Threading;
using UCS.Helpers;

namespace UCS.Core.Threading
{
    internal class ConsoleThread
    {
        #region Private Fields

        private static string Title;
        private static string Tmp;
        private static string Command;

        #endregion Private Fields

        #region Private Properties

        private static Thread T { get; set; }

        #endregion Private Properties

        #region Public Methods

        public static void Start()
        {
            T = new Thread(() =>
            {
                Console.Title = "Ultrapowa Clash Server " + Utils.AssemblyVersion + " - © 2016";
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
                if (Utils.OpenedInstances > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[UCS]    You seem to run UCS more than once.");
                    Console.WriteLine("[UCS]    Aborting..");
                    Console.ResetColor();
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.WriteLine("[UCS]    -> This program is by the Ultrapowa Network development team.");
                Console.WriteLine(
                    "[UCS]    -> You can find the source at www.ultrapowa.com and https://github.com/UltraPowaDev/UCS/");
                Console.WriteLine(
                    "[UCS]    -> Don't forget to visit www.ultrapowa.com daily for the latest news and updates!");
                Console.WriteLine("[UCS]    -> UCS is now starting...");
                Console.WriteLine("");
                Debugger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
                MemoryThread.Start();
                NetworkThread.Start();
                while ((Command = Console.ReadLine()) != null)
                    CommandParser.Parse(Command);
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