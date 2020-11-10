// Copyright 2013-2018 Serilog Contributors
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
using System.Runtime.InteropServices;

#if NETSTANDARD1_3
using System.Net;
#endif

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
    /// </summary>
    public class MachineNameEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;
        readonly string _propertyName;

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string DefaultMachineNamePropertyName = "MachineName";


        /// <summary>
        /// Creates a new instance of the enricher
        /// </summary>
        /// <param name="propertyName">The property name to use for the MachineName</param>
        public MachineNameEnricher(string propertyName = DefaultMachineNamePropertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(GetLogEventProperty(propertyFactory));
        }

        private LogEventProperty GetLogEventProperty(ILogEventPropertyFactory propertyFactory)
        {
            // Don't care about thread-safety, in the worst case the field gets overwritten and one
            // property will be GCed
            if (_cachedProperty == null)
                _cachedProperty = CreateProperty(propertyFactory, _propertyName);

            return _cachedProperty;
        }

        // Qualify as uncommon-path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory, string propertyName)
        {
#if NETSTANDARD1_3
            var machineName = Dns.GetHostName();
#else
            var machineName = Environment.MachineName;
#endif
            return propertyFactory.CreateProperty(propertyName, machineName);
        }
    }
}
