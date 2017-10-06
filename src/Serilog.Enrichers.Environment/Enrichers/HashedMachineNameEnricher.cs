using System;
using System.Security.Cryptography;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a hashed version of a MachineName property containing <see cref="Environment.MachineName"/>.
    /// The hashed version is usefull as a simple installation correlation Id, when you don't want to or can't expose the 
    /// machine name due to privacy issues.
    /// </summary>
    public class HashedMachineNameEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HashedMachineNamePropertyName = "HashedMachineName";

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(HashedMachineNamePropertyName, CalculateMachineNameHash());

            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }

        private static string CalculateMachineNameHash()
        {
#if ENV_USER_NAME
            var machineName = Environment.MachineName;
#else
            var machineName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            if (string.IsNullOrWhiteSpace(machineName))
                machineName = Environment.GetEnvironmentVariable("HOSTNAME");
#endif

            return Convert.ToBase64String(
                MD5.Create().ComputeHash(
                    new UTF8Encoding().GetBytes(machineName)));
        }
    }
}