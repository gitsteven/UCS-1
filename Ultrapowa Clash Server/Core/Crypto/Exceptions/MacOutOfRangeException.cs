using System;

namespace Sodium.Exceptions
{
    public class MacOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public MacOutOfRangeException()
        {
        }

        public MacOutOfRangeException(string message)
          : base(message)
        {
        }

        public MacOutOfRangeException(string message, Exception inner)
          : base(message, inner)
        {
        }

        public MacOutOfRangeException(string paramName, object actualValue, string message)
          : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}