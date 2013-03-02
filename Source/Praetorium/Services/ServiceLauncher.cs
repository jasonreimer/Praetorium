using Praetorium.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace Praetorium.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceLauncher : IDisposable
    {
        /// <summary>
        /// Contains the managed service hosts for the instance.
        /// </summary>
        private readonly List<ServiceHost> _serviceHosts = new List<ServiceHost>();

        /// <summary>
        /// The name of the assembly that contains the service implementation classes.
        /// </summary>
        private readonly string _serviceAssemblyName;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLauncher"/> class.
        /// </summary>
        public ServiceLauncher()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLauncher"/> class.
        /// </summary>
        /// <param name="serviceAssemblyName">Name of the assembly that contains the service implementation classes.</param>
        public ServiceLauncher(string serviceAssemblyName)
        {
            _serviceAssemblyName = serviceAssemblyName;
        }

        /// <summary>
        /// Gets the service hosts contained in this instance.
        /// </summary>
        /// <value>The service hosts.</value>
        public ServiceHost[] ServiceHosts
        {
            get { return _serviceHosts.ToArray(); }
        }

        /// <summary>
        /// Starts all of the managed service hosts based on the system configuration.
        /// </summary>
        public void Start()
        {
            var servicesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;

            foreach (ServiceElement serviceElement in servicesSection.Services)
            {
                Type serviceType = null;

                if (_serviceAssemblyName != null)
                    serviceType = Type.GetType(serviceElement.Name + "," + _serviceAssemblyName, true);
                else
                    serviceType = ReflectionUtility.FindTypeInCurrentAppDomain(serviceElement.Name);

                if (serviceType == null)
                    throw new TypeLoadException("Unable to find the type " + serviceElement.Name);

                Start(serviceType);
            }
        }

        /// <summary>
        /// Starts the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        public void Start(Type serviceType)
        {
            var serviceHost = new ServiceHost(serviceType);
            serviceHost.Open();

            _serviceHosts.Add(serviceHost);
        }

        /// <summary>
        /// Stops all of the managed service hosts.
        /// </summary>
        public void Stop()
        {
            _serviceHosts.ForEach(x => x.Close());
            _serviceHosts.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Stop();
        }
    }
}
