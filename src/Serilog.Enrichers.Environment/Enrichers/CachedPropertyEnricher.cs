using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    public abstract class CachedPropertyEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty { get; set; }

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public abstract string PropertyName { get; }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(PropertyName, GeneratePropertyValue());
            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }

        protected abstract object GeneratePropertyValue();
    }
}
