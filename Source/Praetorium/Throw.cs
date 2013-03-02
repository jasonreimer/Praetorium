using Praetorium.Utilities;
using System;

namespace Praetorium
{
    public static class Throw<TException> where TException : Exception
    {

        private static readonly Func<TException> _createException;

        static Throw()
        {
            try
            {
                _createException = ReflectionUtility.GetNewDelegate<TException>();
            }
            catch
            {
            }
        }

        public static void If(bool condition)
        {
            Throw<InvalidOperationException>.If(_createException == null, m => new InvalidOperationException(m),
                                                "{0} does not have a parameter-less constructor.", typeof(TException).Name);

            if (condition)
                throw _createException();
        }

        public static void If(bool condition, Func<TException> exception)
        {
            Ensure.ArgumentNotNull(() => exception);

            if (condition)
                throw exception();
        }

        public static void If(bool condition, Func<string, TException> exception, string messageFormat, params object[] messageParameters)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNullOrWhiteSpace(() => messageFormat);

            if (condition)
                throw exception(string.Format(messageFormat, messageParameters));
        }

    }
}
