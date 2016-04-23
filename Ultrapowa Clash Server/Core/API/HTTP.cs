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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace UCS.Core
{
    public struct JsonApi
    {
        #region Public Properties

        public Dictionary<string, string> UCS { get; set; }

        #endregion Public Properties
    }

    internal class HTTP
    {
        #region Private Fields

        private HttpListener _listener;
        private int _port;
        private Thread _serverThread;
        private string jsonapp;
        private string mime = "text/plain";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public HTTP(int port)
        {
            Initialize(port);
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public HTTP()
        {
            //get an empty port
            var l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            var port = ((IPEndPoint) l.LocalEndpoint).Port;
            l.Stop();
            Initialize(port);
        }

        #endregion Public Constructors

        #region Public Properties

        public int Port
        {
            get { return _port; }
            private set { }
        }

        public Dictionary<string, string> UCS { get; internal set; }

        #endregion Public Properties

        #region Public Methods

        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        #endregion Public Methods

        #region Private Methods

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private void Handler(string type)
        {
            try
            {
                if (type == "inmemclans")
                    jsonapp = Convert.ToString(ObjectManager.GetInMemoryAlliances().Count);
                else if (type == "inmemplayers")
                    jsonapp = Convert.ToString(ResourcesManager.GetInMemoryLevels().Count);
                else if (type == "onlineplayers")
                    jsonapp = Convert.ToString(ResourcesManager.GetOnlinePlayers().Count);
                else if (type == "totalclients")
                    jsonapp = Convert.ToString(ResourcesManager.GetConnectedClients().Count);
                else if (type == "all")
                {
                    var json = new JsonApi
                    {
                        UCS = new Dictionary<string, string>
                        {
                            {"PatchingServer", ConfigurationManager.AppSettings["patchingServer"]},
                            {"Maintenance", ConfigurationManager.AppSettings["maintenanceMode"]},
                            {"MaintenanceTimeLeft", ConfigurationManager.AppSettings["maintenanceTimeLeft"]},
                            {"ClientVersion", ConfigurationManager.AppSettings["clientVersion"]},
                            {"ServerVersion", Assembly.GetExecutingAssembly().GetName().Version.ToString()},
                            {"OnlinePlayers", Convert.ToString(ResourcesManager.GetOnlinePlayers().Count)},
                            {"InMemoryPlayers", Convert.ToString(ResourcesManager.GetInMemoryLevels().Count)},
                            {"InMemoryClans", Convert.ToString(ObjectManager.GetInMemoryAlliances().Count)},
                            {"TotalConnectedClients", Convert.ToString(ResourcesManager.GetConnectedClients().Count)}
                        }
                    };
                    jsonapp = JsonConvert.SerializeObject(json);
                    mime = "application/json";
                }
                else if (type == "ram")
                {
                    jsonapp = Performances.GetUsedMemory();
                }
                else
                    jsonapp = "OK";
            }
            catch (Exception ex)
            {
                jsonapp = "An exception occured in UCS : \n" + ex;
            }
        }

        private void Initialize(int port)
        {
            _port = port;
            _serverThread = new Thread(Listen);
            _serverThread.Start();
            Console.WriteLine("[UCS]    API has been successfully started");
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://+:" + _port + "/" + ConfigurationManager.AppSettings["ApiKey"] + "/");
            _listener.Start();
            while (true)
            {
                try
                {
                    var context = _listener.GetContext();
                    Process(context);
                }
                catch (Exception ex)
                {
                    // We should do nothing
                }
            }
        }

        private void Process(HttpListenerContext context)
        {
            string[] Apis = { "inmemclans", "inmemplayers", "onlineplayers", "totalclients", "ram", "" };
            var type = context.Request.Url.AbsolutePath.Substring(7).ToLower();

            if (Apis.Contains(type))
            {
                Handler(type);
                try
                {
                    context.Response.ContentType = mime;
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", DateTime.UtcNow.ToString("r"));
                    context.Response.AddHeader("APIVersion", "1.0a");

                    var buffer = new byte[1024 * 16];
                    int nbytes;

                    using (var fstream = GenerateStreamFromString(jsonapp))
                    {
                        while ((nbytes = fstream.Read(buffer, 0, buffer.Length)) > 0)
                            context.Response.OutputStream.Write(buffer, 0, nbytes);
                        fstream.Close();
                    }

                    context.Response.StatusCode = (int) HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            context.Response.OutputStream.Close();
        }

        #endregion Private Methods
    }
}