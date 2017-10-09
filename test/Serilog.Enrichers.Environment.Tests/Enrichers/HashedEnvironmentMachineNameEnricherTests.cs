using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class HashedEnvironmentMachineNameEnricherTests
    {
        [Fact]
        public void HashedEnvironmentMachineNameEnricherIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithHashedMachineName()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an MachineName property");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["HashedMachineName"].LiteralValue());
        }
    }
}