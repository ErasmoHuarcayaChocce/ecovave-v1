using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace ecovave.backend.Health
{
    public class VersionHealthCheck : IHealthCheck
    {
        private readonly VersionConfig _config;

        public VersionHealthCheck(IOptions<VersionConfig> options)
        {
            _config = options.Value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(string.IsNullOrEmpty(_config.Version)
               ? HealthCheckResult.Unhealthy("There is an error with version health check")
               : HealthCheckResult.Healthy($"Version is healthy: {_config.Version}"));
        }
    }
}
