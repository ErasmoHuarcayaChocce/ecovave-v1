using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ecovave.backend.Health
{
    public class ApiHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            //TODO: Implement your own healthcheck logic here
            var isHealthy = true;
            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("I am one healthy microservice API"));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("I am the sad, unhealthy microservice API"));
        }
    }
}
