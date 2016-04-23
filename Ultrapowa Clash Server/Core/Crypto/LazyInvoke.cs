namespace Sodium
{
    public class LazyInvoke<T>
    {
        #region Private Fields

        private readonly string _function;
        private readonly string _library;
        private T _method;
        private bool _missing;

        #endregion Private Fields

        #region Public Constructors

        public LazyInvoke(string function, string library)
        {
            _function = function;
            _library = library;
            _missing = true;
        }

        #endregion Public Constructors

        #region Public Properties

        public T Method
        {
            get
            {
                if (_missing)
                {
                    _method = DynamicInvoke.GetDynamicInvoke<T>(_function, _library);
                    _missing = false;
                }

                return _method;
            }
        }

        #endregion Public Properties
    }
}