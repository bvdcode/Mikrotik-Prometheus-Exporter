using Mikrotik.Exporter.Providers;
using Mikrotik.Exporter.Abstractions;
using Mikrotik.Exporter.Models.Enums;

namespace Mikrotik.Exporter.Metrics
{
    public class SystemMetrics : IMetricGrabber
    {
        public string Name => "SystemMetrics";
        public string Help => "System metrics for Mikrotik devices.";
        public MetricType Type => MetricType.Gauge;

        public double GrabMetrics(MikrotikMetricsProvider mikrotikMetricsProvider)
        {
            //mikrotikMetricsProvider.ExecuteCommand("");
            return 0;
        }
    }
}
