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
    public class SeedOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public SeedOutOfRangeException()
        {
        }

        public SeedOutOfRangeException(string message)
            : base(message)
        {
        }

        public SeedOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public SeedOutOfRangeException(string paramName, object actualValue, string message)
            : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}