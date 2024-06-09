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

namespace Serilog.Enrichers;

/// <summary>
/// Enriches log events with a EnvironmentName property containing the value of the ASPNETCORE_ENVIRONMENT or DOTNET_ENVIRONMENT environment variable.
/// </summary>
sealed class EnvironmentNameEnricher : CachedPropertyEnricher
{
    /// <summary>
    /// The property name added to enriched log events.
    /// </summary>
    const string EnvironmentNamePropertyName = "EnvironmentName";

    protected override LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
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