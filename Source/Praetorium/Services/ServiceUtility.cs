using System;
using System.ServiceModel;

namespace Praetorium.Services
{
    internal static class ServiceUtility
    {
        internal static string GetDefaultEndpointName(Type serviceType)
        {
            var endpointAttrib = serviceType.GetAttribute<DefaultEnpointNameAttribute>();
            string endpointName = null;

            if (endpointAttrib != null)
            {
                endpointName = endpointAttrib.Name;
            }
            else
            {
                var serviceContractAttrib = serviceType.GetAttribute<ServiceContractAttribute>();

                if (serviceContractAttrib != null)
                    endpointName = serviceContractAttrib.Name.TrimToNull();
            }

            return endpointName;
        }

        internal static string GetDefaultEndpointName<TService>() where TService : class
        {
            return GetDefaultEndpointName(typeof(TService));
        }
    }
}
