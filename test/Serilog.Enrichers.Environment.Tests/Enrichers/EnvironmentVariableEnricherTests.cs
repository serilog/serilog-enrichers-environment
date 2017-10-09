using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class EnvironmentVariableValueEnricherTests
    {
        [Fact]
        public void EnvironmentVariableEnricherIsAppliedWhenVariableIsNotDefined()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentVariable("NearlyImpossibleToExistTestVar")
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an Environment Variable property");

            Assert.NotNull(evt);

            Assert.Null(evt.Properties["Env_NearlyImpossibleToExistTestVar"].LiteralValue());
        }

        [Fact]
        public void EnvironmentVariableEnricherIsAppliedWhenVariableExists()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentVariable("USERNAME") //this should exist
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an Environment Variable property");

            Assert.NotNull(evt);

            Assert.NotEmpty((string)evt.Properties["Env_USERNAME"].LiteralValue());
        }
    }
}
