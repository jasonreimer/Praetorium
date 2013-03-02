using System.Collections.Generic;

namespace Praetorium.Services
{
    /// <summary>
    /// Represents a container for additional data used to locate and connect to services.
    /// </summary>
    public class ServiceLocationProperties
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocationProperties"/> class.
        /// </summary>
        public ServiceLocationProperties()
        {
            ExtendedProperties = new Dictionary<string,string>();
        }

        /// <summary>
        /// Gets or sets the name of the service configuration.
        /// </summary>
        /// <value>The name of the service configuration.</value>
        public string ServiceConfigurationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>The name of the service.</value>
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the service location (typically a url to the service).
        /// </summary>
        /// <value>The service location.</value>
        public string ServiceLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the extended properties.
        /// </summary>
        /// <value>The extended properties.</value>
        public IDictionary<string, string> ExtendedProperties
        {
            get;
            private set;
        }

    }
}
