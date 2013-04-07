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

using Praetorium.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace $rootnamespace$.FaultBuilders
{
    public class DefaultFaultBuilder : IFaultBuilder
    {
        public bool Supports(Exception context)
        {
            return true;
        }

        public MessageFault Create(Exception context)
        {
            // adjust and resource message as desired 
            var message = "The service experienced an unexpected error.";
            var faultException = new FaultException(message);
            var messageFault = faultException.CreateMessageFault();

            return messageFault;
        }
    }
}
