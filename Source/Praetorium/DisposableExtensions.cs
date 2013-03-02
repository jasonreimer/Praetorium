using System;

namespace Praetorium
{
    public static class DisposableExtensions
    {
        public static void SafeDispose(this IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
            }
        }
    }
}
