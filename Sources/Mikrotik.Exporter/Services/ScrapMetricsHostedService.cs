using EasyExtensions.Helpers;
using EasyExtensions.Services;
using Mikrotik.Exporter.Providers;
using Mikrotik.Exporter.Abstractions;

namespace Mikrotik.Exporter.Services
{
    public class ScrapMetricsHostedService(ILogger<ScrapMetricsHostedService> _logger, 
        CpuUsageService _cpuUsageService, MikrotikMetricsProvider _provider, IConfiguration _configuration) : BackgroundService
    {
        public IReadOnlyDictionary<string, double> Metrics => _scrapMetrics;

        private readonly TimeSpan _scrapMetricsInterval = TimeSpan.FromSeconds(60);
        private readonly Dictionary<string, double> _scrapMetrics = new(StringComparer.OrdinalIgnoreCase);
        private readonly string _scrapMetricsPrefix = _configuration["MIKROTIK_SCRAP_METRICS_PREFIX"] ?? "mikrotik_exporter_";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _scrapMetrics[_scrapMetricsPrefix + "version"] = 1.0;
            _logger.LogInformation("Metrics scrapper is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogDebug("Metrics scrapper is executing loop.");
                    await ExecuteLoopAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while scraping metrics.");
                }
                _logger.LogDebug("Metrics scrapper is sleeping for {Interval} seconds.", _scrapMetricsInterval.TotalSeconds);
                await Task.Delay(_scrapMetricsInterval, stoppingToken);
            }
        }

        private Task ExecuteLoopAsync(CancellationToken stoppingToken)
        {
            _scrapMetrics[_scrapMetricsPrefix + "cpu_usage"] = _cpuUsageService.GetUsage().CpuUsage;
            _scrapMetrics[_scrapMetricsPrefix + "uptime"] = _cpuUsageService.GetUsage().Uptime.TotalSeconds;

            var types = ReflectionHelpers.GetTypesOfInterface<IMetricGrabber>();
            _logger.LogDebug("Found {Count} metric grabbers.", types.Count());
            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is IMetricGrabber metricGrabber)
                {
                    _logger.LogDebug("Executing metric grabber: {Name}", metricGrabber.Name);
                    double metrics = metricGrabber.GrabMetrics(_provider);
                    _scrapMetrics[_scrapMetricsPrefix + metricGrabber.Name] = metrics;
                }
                else
                {
                    _logger.LogWarning("Failed to create instance of metric grabber: {Name}", type.Name);
                }
            }
            _logger.LogDebug("Metrics scrapper loop executed successfully. Metrics count: {Count}", _scrapMetrics.Count);
            return Task.CompletedTask;
        }
    }
}