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

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a EnvironmentName property containing the value of the ASPNETCORE_ENVIRONMENT or DOTNET_ENVIRONMENT environment variable.
    /// </summary>
    public class EnvironmentNameEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string EnvironmentNamePropertyName = "EnvironmentName";

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
                _cachedProperty = CreateProperty(propertyFactory);

            return _cachedProperty;
        }

        // Qualify as uncommon-path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            }

            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = "Production";
            }

            return propertyFactory.CreateProperty(EnvironmentNamePropertyName, environmentName);
        }
    }
}
