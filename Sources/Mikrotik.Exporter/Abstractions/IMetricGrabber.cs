using Mikrotik.Exporter.Models.Enums;
using Mikrotik.Exporter.Providers;

namespace Mikrotik.Exporter.Abstractions
{
    public interface IMetricGrabber
    {
        string Name { get; }
        string Help { get; }
        MetricType Type { get; }

        double GrabMetrics(MikrotikMetricsProvider mikrotikMetricsProvider);
    }
}