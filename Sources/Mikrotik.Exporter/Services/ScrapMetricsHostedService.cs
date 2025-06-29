using EasyExtensions.Services;

namespace Mikrotik.Exporter.Services
{
    public class ScrapMetricsHostedService(ILogger<ScrapMetricsHostedService> _logger, 
        CpuUsageService _cpuUsageService) : BackgroundService
    {
        public IReadOnlyDictionary<string, double> Metrics => _scrapMetrics;

        private readonly Dictionary<string, double> _scrapMetrics = new(StringComparer.OrdinalIgnoreCase);
        private readonly TimeSpan _scrapMetricsInterval = TimeSpan.FromSeconds(10);
        private const string _scrapMetricsPrefix = "mikrotik_exporter_";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Metrics scrapper is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ExecuteLoopAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while scraping metrics.");
                }
                await Task.Delay(_scrapMetricsInterval, stoppingToken);
            }
        }

        private async Task ExecuteLoopAsync(CancellationToken stoppingToken)
        {
            _scrapMetrics[_scrapMetricsPrefix + "version"] = 1.0;
            _scrapMetrics[_scrapMetricsPrefix + "cpu_usage"] = _cpuUsageService.GetUsage().CpuUsage;
            _scrapMetrics[_scrapMetricsPrefix + "uptime"] = _cpuUsageService.GetUsage().Uptime.TotalSeconds;
        }
    }
}