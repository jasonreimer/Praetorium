using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class BasicInstanceProvider : IInstanceProvider
    {
        private readonly Func<object> _serviceFactory;

        public BasicInstanceProvider(Func<object> serviceFactory)
        {
            Ensure.ArgumentNotNull(() => serviceFactory, ref _serviceFactory);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _serviceFactory();
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}
