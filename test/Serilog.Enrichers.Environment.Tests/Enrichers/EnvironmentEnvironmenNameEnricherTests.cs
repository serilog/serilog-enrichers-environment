using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class EnvironmentEnvironmenNameEnricherTests
    {
        [Fact]
        public void EnvironmentNameEnricherIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentName()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an EnvironmenName property with the value of the DOTNET_ENVIRONMENT or ASPNETCORE_ENVIRONMENT environment variable.");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["EnvironmentName"].LiteralValue());
        }
    }
}
