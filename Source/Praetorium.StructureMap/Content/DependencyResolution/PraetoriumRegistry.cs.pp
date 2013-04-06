using Praetorium.Configuration;
using Praetorium.Eventing;
using Praetorium.Logging;
using Praetorium.Services;
using StructureMap.Configuration.DSL;
using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace $rootnamespace$.DependencyResolution
{
    public class PraetoriumRegistry : Registry
    {
        public PraetoriumRegistry()
        {
            // common funcs
            For<Func<Type,object>>().Use(c => t => c.GetInstance(t));
            For<Func<Type,string,object>>().Use(c => (type,name) => c.GetInstance(type, name));
            
            // configuration
            For<IConfigReader>().Use<ConfigurationManagerReader>();
            
            // logging
            For<ILoggingConfigurer>().Use<LoggingConfigurerStub>(); // replace if using another logging framework
            For<ILoggerFactory>().Use<TraceLoggerFactory>(); // replace if using another logging framework
            For<ILogger>().Use<TraceLogger>(); // replace if using another logging framework or remove altogether
            ForSingletonOf<IExceptionFormatterFactory>().Use<ExceptionFormatterFactory>();
            For<IExceptionFormatter>().Add<FaultExceptionFormatter>();

            // Events
            ForSingletonOf<IEventAggregator>().Use<EventAggregator>();
            
            // WCF
            For<IServiceRegistry>().Use<ServiceRegistry>();
            For<IServiceProxy>().Use<ServiceProxy>();
            For<IFaultFactory>().Use<FaultFactory>();
            //For<IServiceConfigurer>().Use<XXX>();
            
            // Fault Builders
            //For<IFaultBuilder>().Add<XXX>();
        }
        
        private void RegisterServiceBehaviors()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.AddAllTypesOf<IServiceBehavior>();
            });
        }

        private void RegisterClientInspector<TInspector>() where TInspector : class, IClientMessageInspector
        {
            For<TInspector>().Use<TInspector>();
            For<IEndpointBehavior>().Add<ClientInspectorBehavior<TInspector>>();
        }

        private void RegisterDispatchInspector<TInspector>() where TInspector : class, IDispatchMessageInspector
        {
            For<TInspector>().Use<TInspector>();
            For<IServiceBehavior>().Add<DispatchInspectorBehavior<TInspector>>();
        }
    }
}