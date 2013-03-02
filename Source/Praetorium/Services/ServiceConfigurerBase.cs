using System;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace Praetorium.Services
{
    public abstract class ServiceConfigurerBase : IServiceConfigurer
    {
        private readonly IList<IServiceBehavior> _serviceBehaviors;

        public ServiceConfigurerBase(IServiceBehavior[] serviceBehaviors)
        {
            _serviceBehaviors = new List<IServiceBehavior>(serviceBehaviors ?? new IServiceBehavior[0]);
        }

        protected IList<IServiceBehavior> ServiceBehaviors { get { return _serviceBehaviors; } }

        public virtual void Configure(ServiceDescription description)
        {
            Ensure.ArgumentNotNull(() => description);

            ConfigureBehaviors(description);
        }

        protected virtual void ConfigureBehaviors(ServiceDescription description)
        {
            ApplyServiceBehaviors(description);
        }

        protected virtual void ApplyServiceBehaviors(ServiceDescription description)
        {
            foreach (var behavior in GetBehaviorsForService(description.ServiceType))
            {
                if (description.Behaviors.Contains(behavior.GetType()))
                    description.Behaviors.Remove(behavior.GetType());

                description.Behaviors.Add(behavior);
            }
        }

        protected virtual IEnumerable<IServiceBehavior> GetBehaviorsForService(Type serviceType)
        {
            return _serviceBehaviors;
        }
    }
}
