using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace Mikrotik.Exporter.Services
{
    public class ScrapMetricsHostedService(ILogger<ScrapMetricsHostedService> _logger) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ScrapMetricsHostedService is starting.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}