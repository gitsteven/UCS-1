using System;

namespace Sodium.Exceptions
{
    public class SaltOutOfRangeException : ArgumentOutOfRangeException
    {
        #region Public Constructors

        public SaltOutOfRangeException()
        {
        }

        public SaltOutOfRangeException(string message)
          : base(message)
        {
        }

        public SaltOutOfRangeException(string message, Exception inner)
          : base(message, inner)
        {
        }

        public SaltOutOfRangeException(string paramName, object actualValue, string message)
          : base(paramName, actualValue, message)
        {
        }

        #endregion Public Constructors
    }
}