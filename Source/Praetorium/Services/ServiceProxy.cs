using Praetorium.Logging;
using System;
using System.ServiceModel;

namespace Praetorium.Services
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly IServiceRegistry _serviceRegistry;
        private readonly ILogger _logger;

        public ServiceProxy(IServiceRegistry serviceRegistry, ILoggerFactory loggerFactory)
        {
            Ensure.ArgumentNotNull(() => serviceRegistry, ref _serviceRegistry);
            Ensure.ArgumentNotNull(() => loggerFactory);

            _logger = loggerFactory.Get<ServiceProxy>();
        }

        public void Use<TServiceInterface>(Action<TServiceInterface> action) where TServiceInterface : class
        {
            var proxyType = typeof(TServiceInterface);
            var service = _serviceRegistry.GetProxy<TServiceInterface>();
            var channel = service as IClientChannel;

            try
            {
                action(service);

                CloseChannel(channel, proxyType);
            }
            catch
            {
                AbortOrCloseChannel(channel, proxyType);
                throw;
            }
        }

        public TResult Use<TServiceInterface, TResult>(Func<TServiceInterface, TResult> action) where TServiceInterface : class
        {
            var proxyType = typeof(TServiceInterface);
            var service = _serviceRegistry.GetProxy<TServiceInterface>();
            var channel = service as IClientChannel;

            try
            {
                var value = action(service);

                CloseChannel(channel, proxyType);

                return value;
            }
            catch
            {
                AbortOrCloseChannel(channel, proxyType);
                throw;
            }
        }

        /// <summary>
        /// Closes the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="proxyType">Type of the proxy.</param>
        private void CloseChannel(IClientChannel channel, Type proxyType)
        {
            if (channel != null)
            {
                _logger.Debug("Closing client channel for '{0}' ...", proxyType.Name);
                channel.Close();
                _logger.Debug("Client channel for '{0}' closed.", proxyType.Name);
            }
        }

        /// <summary>
        /// Aborts or closes channel depending on the"/>
        /// <see cref="P:System.ServiceModel.IClientChannel.State"/> property
        /// of the <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="proxyType">Type of the proxy.</param>
        private void AbortOrCloseChannel(IClientChannel channel, Type proxyType)
        {
            if (channel != null)
            {
                if (channel.State == CommunicationState.Faulted)
                {
                    _logger.Debug("Aborting client channel for '{0}' ...", proxyType.Name);

                    channel.Abort();

                    _logger.Debug("Client channel for '{0}' aborted.", proxyType.Name);
                }
                else
                {
                    _logger.Debug("Closing client channel for '{0}' ...", proxyType.Name);

                    channel.Close();

                    _logger.Debug("Client channel for '{0}' closed.", proxyType.Name);
                }
            }
        }

    }

}
