// Copyright 2013-2016 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
 

using System;
using Serilog.Core;
using Serilog.Events;
using System.Runtime.InteropServices;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
    /// </summary>
    public class MachineNameEnricher : CachedPropertyEnricher
    {
        public override string PropertyName => "MachineName";

        protected override object GeneratePropertyValue()
        {
#if ENV_USER_NAME
            return Environment.MachineName;
#else
            var machineName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            if (string.IsNullOrWhiteSpace(machineName))
                machineName = Environment.GetEnvironmentVariable("HOSTNAME");

            return machineName;
#endif
        }
    }
} 
