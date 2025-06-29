using Microsoft.AspNetCore.Mvc;
using Mikrotik.Exporter.Services;

namespace Mikrotik.Exporter.Controllers
{
    public class MetricsController(ScrapMetricsHostedService _metrics) : ControllerBase
    {
        [HttpGet("/metrics")]
        [Produces("text/plain", "application/json")]
        public IActionResult GetMetrics()
        {
            string result = _metrics.Metrics
                .Select(kv => $"{kv.Key} {kv.Value}")
                .Aggregate((current, next) => current + Environment.NewLine + next);

            return Content(result, "text/plain");
        }
    }
}
