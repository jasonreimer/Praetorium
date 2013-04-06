using Praetorium;
using Praetorium.Logging;
using StructureMap;
using System;
using System.Diagnostics;
using $rootnamespace$.DependencyResolution;

namespace $rootnamespace$.Configuration
{
    public class Bootstrapper
    {
        public event Action ContainerConfigured;

        private readonly TraceSource _traceSource = new TraceSource("Bootstrapper");

        public Bootstrapper(Action<ConfigurationExpression> configurationAction)
            : this(ObjectFactory.Container, configurationAction)
        {
        }

        public Bootstrapper(IContainer container, Action<ConfigurationExpression> configurationAction)
        {
            Ensure.ArgumentNotNull(() => container);
            Ensure.ArgumentNotNull(() => configurationAction);

            Container = container;
            ConfigurationAction = configurationAction;
        }

        public IContainer Container
        {
            get;
            private set;
        }

        protected Action<ConfigurationExpression> ConfigurationAction
        {
            get;
            private set;
        }

        protected TraceSource TraceSource
        {
            get { return _traceSource; }
        }

        public void Run()
        {
            try
            {
                _traceSource.TraceEvent(TraceEventType.Start, 0, "Bootstrapper running...");

                InternalRun();

                _traceSource.TraceEvent(TraceEventType.Verbose, 0, "Bootstrapper run completed.");
            }
            catch (Exception ex)
            {
                _traceSource.TraceEvent(TraceEventType.Critical, 0, "Bootstrapper configuration failed due to the following exception: " + ex.ToString());
                throw;
            }
        }

        protected virtual void InternalRun()
        {
            ConfigureContainer();
            InitializeLogging();
            InitializeAndStartServices();
        }

        protected virtual void ConfigureContainer()
        {
            Container.Configure(ConfigurationAction);
            OnContainerConfigured();
        }

        protected void InitializeAndStartServices()
        {
            _traceSource.TraceEvent(TraceEventType.Verbose, 0, "Starting startables...");

            Container.StartServices();

            _traceSource.TraceEvent(TraceEventType.Verbose, 0, "Startables started.");
        }

        protected virtual void InitializeLogging()
        {
            Container.GetInstance<ILoggingConfigurer>().Configure();
        }

        protected virtual void OnContainerConfigured()
        {
            if (ContainerConfigured != null)
            {
                ContainerConfigured();
            }
        }
    }
}