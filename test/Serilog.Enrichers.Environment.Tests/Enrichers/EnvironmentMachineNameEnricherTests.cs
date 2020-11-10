using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class EnvironmentMachineNameEnricherTests
    {
        [Fact]
        public void EnvironmentMachineNameEnricherIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an MachineName property");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["MachineName"].LiteralValue());
        }

        [Fact]
        public void EnvironmentCustomMachineNameEnricherIsApplied()
        {
            string customPropertyName = "host";
            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .Enrich.WithMachineName(customPropertyName)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an MachineName property");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["host"].LiteralValue());
        }
    }
}
