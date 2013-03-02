using System;

namespace Praetorium.Services
{
    public interface IServiceProxy
    {
        void Use<TService>(Action<TService> action) where TService : class;

        TResult Use<TService, TResult>(Func<TService, TResult> action) where TService : class;
    }
}
