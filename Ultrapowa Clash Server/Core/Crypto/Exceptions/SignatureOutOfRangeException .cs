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

namespace Sodium.Exceptions
{
    public class SignatureOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public SignatureOutOfRangeException()
        {
        }

        public SignatureOutOfRangeException(string message)
          : base(message)
        {
        }

        public SignatureOutOfRangeException(string message, Exception inner)
          : base(message, inner)
        {
        }

        public SignatureOutOfRangeException(string paramName, object actualValue, string message)
          : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}