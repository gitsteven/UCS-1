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

using Ionic.Zlib;
using System.Collections.Generic;
using System.Configuration;
using UCS.Helpers;
using static UCS.Core.Debugger;

namespace UCS.PacketProcessing.Messages.Server
{
    //Packet 20103
    internal class LoginFailedMessage : Message
    {
        #region Public Constructors

        public LoginFailedMessage(PacketProcessing.Client client) : base(client)
        {
            SetMessageType(20103);
            SetUpdateURL(ConfigurationManager.AppSettings["UpdateUrl"]);
            SetMessageVersion(2);
            //SetReason("UCS Developement Team");
            // 8  : new game version available (removeupdateurl)
            // 10 : maintenance
            // 11 : banni temporairement
            // 12 : played too much
            // 13 : compte verrouillé
        }

        #endregion Public Constructors

        #region Private Fields

        private string m_vContentURL;
        private int m_vErrorCode;
        private string m_vReason;
        private string m_vRedirectDomain;
        private int m_vRemainingTime;
        private string m_vResourceFingerprintData;
        private string m_vUpdateURL;

        #endregion Private Fields

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();
            if (Client.CState == 0)
            {
                pack.AddInt32(m_vErrorCode);
                pack.AddString(m_vResourceFingerprintData);
                pack.AddString(m_vRedirectDomain);
                pack.AddString(m_vContentURL);
                pack.AddString(m_vUpdateURL);
                pack.AddString(m_vReason);
                pack.AddInt32(-1);
                pack.Add(0);
                SetData(pack.ToArray());
            }
            else
            {
                pack.AddInt32(m_vErrorCode);
                pack.AddString(m_vResourceFingerprintData);
                pack.AddString(m_vRedirectDomain);
                pack.AddString(m_vContentURL);
                pack.AddString(m_vUpdateURL);
                pack.AddString(m_vReason);
                pack.AddInt32(m_vRemainingTime);
                pack.Add(0);
                pack.AddRange(ZlibStream.CompressString(m_vResourceFingerprintData));
                pack.AddString("")
                pack.AddInt32(2);
                Encrypt(pack.ToArray());
            }
        }
        public void RemainingTime(int code)
        {
            m_vRemainingTime = code;
        }

        public void SetContentURL(string url)
        {
            m_vContentURL = url;
        }

        public void SetErrorCode(int code)
        {
            m_vErrorCode = code;
        }

        public void SetReason(string reason)
        {
            m_vReason = reason;
        }

        public void SetRedirectDomain(string domain)
        {
            m_vRedirectDomain = domain;
        }

        public void SetResourceFingerprintData(string data)
        {
            m_vResourceFingerprintData = data;
        }

        public void SetUpdateURL(string url)
        {
            m_vUpdateURL = url;
        }

        #endregion Public Methods
    }
}