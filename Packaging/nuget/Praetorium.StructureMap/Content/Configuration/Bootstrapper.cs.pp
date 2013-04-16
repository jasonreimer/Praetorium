// ----------------------------------------------------------------------------
// Copyright 2013 Jason Reimer
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------

using Praetorium;
using Praetorium.Logging;
using StructureMap;
using System;
using System.Diagnostics;
using $rootnamespace$.DependencyResolution;

namespace $rootnamespace$.Configuration
{
    public partial class Bootstrapper
    {
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
    }
}