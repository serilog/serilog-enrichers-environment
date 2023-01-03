// Copyright 2013-2022 Serilog Contributors
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
using System.Runtime.CompilerServices;

#if NETSTANDARD1_3
using System.Net;
#endif

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
    /// </summary>
    public class MachineNameEnricher : CachedPropertyEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string MachineNamePropertyName = "MachineName";

        // Qualify as uncommon-path
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected override LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
        {
#if NETSTANDARD1_3
            var machineName = Dns.GetHostName();
#else
            var machineName = Environment.MachineName;
#endif
            return propertyFactory.CreateProperty(MachineNamePropertyName, machineName);
        }
    }
}
