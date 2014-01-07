using System;
using System.Threading.Tasks;

namespace Praetorium.Services
{
    public interface IServiceProxy
    {
        void Use<TService>(Action<TService> action) where TService : class;

        Task UseAsync<TService>(Func<TService, Task> action) where TService : class;

        TResult Use<TService, TResult>(Func<TService, TResult> action) where TService : class;

        Task<TResult> UseAsync<TService, TResult>(Func<TService, Task<TResult>> action) where TService : class;
    }
}
