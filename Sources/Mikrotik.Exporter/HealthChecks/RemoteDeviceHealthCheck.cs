using Mikrotik.Exporter.Providers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mikrotik.Exporter.HealthChecks
{
    public class RemoteDeviceHealthCheck(MikrotikMetricsProvider _provider) : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var result = _provider.IsConnected()
                ? HealthCheckResult.Healthy($"{nameof(MikrotikMetricsProvider)} is connected.")
                : HealthCheckResult.Healthy($"{nameof(RemoteDeviceHealthCheck)} is down.");
            return Task.FromResult(result);
        }
    }
}