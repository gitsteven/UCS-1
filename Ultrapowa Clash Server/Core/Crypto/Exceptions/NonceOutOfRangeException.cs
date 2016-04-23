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
    public class NonceOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public NonceOutOfRangeException()
        {
        }

        public NonceOutOfRangeException(string message)
          : base(message)
        {
        }

        public NonceOutOfRangeException(string message, Exception inner)
          : base(message, inner)
        {
        }

        public NonceOutOfRangeException(string paramName, object actualValue, string message)
          : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}