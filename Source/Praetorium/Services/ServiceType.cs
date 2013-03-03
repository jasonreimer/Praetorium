using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Praetorium.Services
{
    internal class ServiceType<TService> : IDisposable where TService : class
    {

        public readonly string DefaultEndpointName;
        public readonly bool HasDefaultEndpointName;
        public readonly string TypeName;
        public readonly string FullTypeName;
        public readonly string Name;
        public readonly string Namespace;
        public readonly bool HasName;
        public readonly bool IsInterface;

        private readonly object _channelFactoriesLockObject = new object();
        private ChannelFactory<TService> _defaultChannelFactory;
        private Dictionary<string, ChannelFactory<TService>> _channelFactories = new Dictionary<string, ChannelFactory<TService>>();
        private bool _isDisposed = false;

        public ServiceType()
        {
            var serviceType = typeof(TService);
            var serviceContractAttrib = serviceType.GetAttribute<ServiceContractAttribute>();

            if (serviceContractAttrib == null)
            {
                throw Errors.TypeNotServiceContract(serviceType.FullName);
            }

            DefaultEndpointName = ServiceUtility.GetDefaultEndpointName(serviceType);
            HasDefaultEndpointName = DefaultEndpointName != null;
            TypeName = serviceType.Name;
            FullTypeName = serviceType.FullName;
            IsInterface = serviceType.IsInterface;
            Name = serviceContractAttrib.Name.TrimToNull();
            Namespace = serviceContractAttrib.Namespace.TrimToNull();
            HasName = Name != null;

            if (HasDefaultEndpointName)
            {
                _defaultChannelFactory = CreateChannelFactory(DefaultEndpointName);
            }
        }
        
        public TService CreateChannel()
        {
            CheckDisposed();
            CheckDefaultChannel();

            return _defaultChannelFactory.CreateChannel();
        }

        public TService CreateChannel(EndpointAddress endpointAddress)
        {
            Ensure.ArgumentNotNull(() => endpointAddress);
            CheckDisposed();
            CheckDefaultChannel();

            return _defaultChannelFactory.CreateChannel(endpointAddress);
        }

        public TService CreateChannel(EndpointAddress endpointAddress, Uri via)
        {
            Ensure.ArgumentNotNull(() => endpointAddress);
            Ensure.ArgumentNotNull(() => via);
            CheckDisposed();
            CheckDefaultChannel();

            return _defaultChannelFactory.CreateChannel(endpointAddress, via);
        }

        public TService CreateChannel(string endpointConfigurationName)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => endpointConfigurationName);
            CheckDisposed();

            var channelFactory = GetChannelFactory(endpointConfigurationName);

            return channelFactory.CreateChannel();
        }

        public TService CreateChannel(string endpointConfigurationName, EndpointAddress endpointAddress)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => endpointConfigurationName);
            Ensure.ArgumentNotNull(() => endpointAddress);
            CheckDisposed();

            var channelFactory = GetChannelFactory(endpointConfigurationName);

            return channelFactory.CreateChannel(endpointAddress);
        }

        public TService CreateChannel(string endpointConfigurationName, EndpointAddress endpointAddress, Uri via)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => endpointConfigurationName);
            Ensure.ArgumentNotNull(() => endpointAddress);
            Ensure.ArgumentNotNull(() => via);
            CheckDisposed();

            var channelFactory = GetChannelFactory(endpointConfigurationName);

            return channelFactory.CreateChannel(endpointAddress, via);
        }

        public TService CreateChannel(ServiceLocationProperties locationProperties)
        {
            Ensure.ArgumentNotNull(() => locationProperties);
            CheckDisposed();

            TService channel = null;
            var endpointConfigurationName = locationProperties.ServiceConfigurationName.ReplaceNullOrWhiteSpace(DefaultEndpointName);
            var channelFactory = GetChannelFactory(endpointConfigurationName);

            if (!locationProperties.UserName.IsNullOrWhiteSpace())
            {
                channelFactory.Credentials.UserName.UserName = locationProperties.UserName;
                channelFactory.Credentials.UserName.Password = locationProperties.Password;
            }

            if (!locationProperties.ServiceLocation.IsNullOrEmpty())
            {
                var address = new EndpointAddress(locationProperties.ServiceLocation);

                channel = channelFactory.CreateChannel(address);
            }
            else
            {
                channel = channelFactory.CreateChannel();
            }

            return Ensure.ReturnIsNotNull(channel);
        }

        public void Dispose()
        {
            ReleaseChannelFactories();
        }

        protected virtual void ReleaseChannelFactories()
        {
            lock (_channelFactoriesLockObject)
            {
                if (!_isDisposed)
                {
                    CloseChannelFactory(_defaultChannelFactory);
                    _channelFactories.Values.ForEach(channelFactory => CloseChannelFactory(channelFactory));

                    _defaultChannelFactory = null;
                    _channelFactories = null;
                    _isDisposed = true;
                }
            }
        }

        protected virtual ChannelFactory<TService> GetChannelFactory(string endpointConfigurationName)
        {
            Ensure.ArgumentNotNull(() => endpointConfigurationName);

            ChannelFactory<TService> channelFactory = null;

            if (DefaultEndpointName == endpointConfigurationName)
            {
                channelFactory = _defaultChannelFactory;
            }
            else
            {
                lock (_channelFactoriesLockObject)
                {
                    if (!_channelFactories.TryGetValue(endpointConfigurationName, out channelFactory))
                    {
                        channelFactory = CreateChannelFactory(endpointConfigurationName);
                        _channelFactories.Add(endpointConfigurationName, channelFactory);
                    }
                }
            }

            return Ensure.ReturnIsNotNull(channelFactory);
        }

        protected ChannelFactory<TService> CreateChannelFactory(string endpointConfigurationName)
        {
            try
            {
                return new ChannelFactory<TService>(endpointConfigurationName);
            }
            catch (Exception exception)
            {
                throw Errors.ChannelFactoryCouldNotBeCreated(typeof(TService), endpointConfigurationName, exception);
            }
        }

        protected void CheckDisposed()
        {
            if (_isDisposed)
            {
                throw Errors.ChannelFactoriesDisposed();
            }
        }

        protected void CheckDefaultChannel()
        {
            if (_defaultChannelFactory == null || _defaultChannelFactory.Endpoint.Address == null)
            {
                throw Errors.ChannelFactoryWithNoEndpointException(FullTypeName);
            }
        }

        protected void CloseChannelFactory(ChannelFactory<TService> channelFactory)
        {
            switch (channelFactory.State)
            {
                case CommunicationState.Created:
                case CommunicationState.Opened:
                    channelFactory.Close();
                    break;
                case CommunicationState.Faulted:
                    channelFactory.Abort();
                    break;
            }
        }

    }
}
