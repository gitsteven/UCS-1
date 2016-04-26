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

namespace UCS.Core.Crypto.Sodium.Exceptions
{
    public class BytesOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public BytesOutOfRangeException()
        {
        }

        public BytesOutOfRangeException(string message)
            : base(message)
        {
        }

        public BytesOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BytesOutOfRangeException(string paramName, object actualValue, string message)
            : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}