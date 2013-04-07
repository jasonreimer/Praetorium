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

            // events
            ForSingletonOf<IEventAggregator>().Use<EventAggregator>();
            
            // WCF
            For<IServiceRegistry>().Use<ServiceRegistry>();
            For<IServiceProxy>().Use<ServiceProxy>();
        }

        private void RegisterClientInspector<TInspector>() where TInspector : class, IClientMessageInspector
        {
            For<TInspector>().Use<TInspector>();
            For<IEndpointBehavior>().Add<ClientInspectorBehavior<TInspector>>();
        }
    }
}