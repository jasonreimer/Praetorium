using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Praetorium.Services
{
    public interface IServiceRegistry
    {

        /// <summary>
        /// Gets an instance of <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// An instance of the service.
        /// </returns>
        TService GetProxy<TService>() where TService : class;

        /// <summary>
        /// Gets an instance of <typeparamref name="TService"/> using the specified 
        /// <paramref name="serviceConfigurationName"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceConfigurationName">
        /// Name of the service configuration in the service configuration system.
        /// </param>
        /// <returns>
        /// An instance of the service.
        /// </returns>
        TService GetProxy<TService>(string serviceConfigurationName) where TService : class;

        /// <summary>
        /// Gets an instance of <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="locationProperties">The location properties.</param>
        /// <returns>
        /// An instance of the service.
        /// </returns>
        TService GetProxy<TService>(ServiceLocationProperties locationProperties) where TService : class;

    }
}
