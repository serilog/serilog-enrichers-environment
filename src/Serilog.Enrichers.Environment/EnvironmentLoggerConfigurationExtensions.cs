﻿// Copyright 2013-2016 Serilog Contributors
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
using Serilog.Configuration;
using Serilog.Enrichers;

namespace Serilog;

/// <summary>
/// Extends <see cref="LoggerConfiguration"/> to add enrichers for <see cref="System.Environment"/>.
/// capabilities.
/// </summary>
public static class EnvironmentLoggerConfigurationExtensions
{
    /// <summary>
    /// Enrich log events with a EnvironmentName property containing the value of the <code>ASPNETCORE_ENVIRONMENT</code>
    /// or <code>DOTNET_ENVIRONMENT</code> environment variable.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithEnvironmentName(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
        return enrichmentConfiguration.With<EnvironmentNameEnricher>();
    }

    /// <summary>
    /// Enrich log events with a MachineName property containing the current <see cref="Environment.MachineName"/>.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithMachineName(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
        return enrichmentConfiguration.With<MachineNameEnricher>();
    }

    /// <summary>
    /// Enriches log events with an EnvironmentUserName property containing <see cref="Environment.UserDomainName"/>.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithEnvironmentUserName(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
        return enrichmentConfiguration.With<EnvironmentUserNameEnricher>();
    }
        
    /// <summary>
    /// Enriches log events with an property containing the value of the specified Environment Variable using
    /// <see cref="Environment.GetEnvironmentVariable(string)"/>.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <param name="environmentVariableName">The name of the Environment Variable</param>
    /// <param name="propertyName">The Optional name of the property. If empty <paramref name="environmentVariableName"/> is used</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithEnvironmentVariable(
        this LoggerEnrichmentConfiguration enrichmentConfiguration, string environmentVariableName, string? propertyName = null)
    {
        if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
        var environmentVariableEnricher = new EnvironmentVariableEnricher(environmentVariableName, propertyName);
        return enrichmentConfiguration.With(environmentVariableEnricher);
    }
}