using System;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class EnvironmentVariableEnricherTests
    {
        
        [Fact]
        public void EnvironmentVariable_EnvVariableMissing_EnricherAcceptsNullValue()
        {
            Environment.SetEnvironmentVariable("CUSTOM_VARIABLE", null);
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentVariable("CUSTOM_VARIABLE")
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("EnvironmentVariable CUSTOM_VARIABLE missing.");

            Assert.NotNull(evt);
            Assert.Null((string)evt.Properties["CUSTOM_VARIABLE"].LiteralValue());
        }
        
        [Fact]
        public void EnvironmentVariable_PropertyValue_EnricherUsesSpecificPropertyValue()
        {
            Environment.SetEnvironmentVariable("CUSTOM_VARIABLE", null);
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentVariable("CUSTOM_VARIABLE", "PropertyName")
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("PropertyName has been overriden");

            Assert.NotNull(evt);
            Assert.Null((string)evt.Properties["PropertyName"].LiteralValue());
        }
        
        [Fact]
        public void EnvironmentVariable_EnvVariableMissing_EnricherUsesSetValue()
        {
            Environment.SetEnvironmentVariable("CUSTOM_VARIABLE", "random-value");
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentVariable("CUSTOM_VARIABLE")
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("EnvironmentVariable CUSTOM_VARIABLE has a value");

            Assert.NotNull(evt);
            Assert.Equal("random-value", (string)evt.Properties["CUSTOM_VARIABLE"].LiteralValue());
        }
    }
}
