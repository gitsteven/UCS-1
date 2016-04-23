using System;

namespace Sodium.Exceptions
{
    public class KeyOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public KeyOutOfRangeException()
        {
        }

        public KeyOutOfRangeException(string message)
          : base(message)
        {
        }

        public KeyOutOfRangeException(string message, Exception inner)
          : base(message, inner)
        {
        }

        public KeyOutOfRangeException(string paramName, object actualValue, string message)
          : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}