using Microsoft.AspNetCore.Mvc;

namespace Mikrotik.Exporter.Controllers
{
    public class MetricsController : ControllerBase
    {
        [HttpGet("/metrics")]
        [Produces("text/plain", "application/json")]
        public IActionResult GetMetrics()
        {
            var metrics = "# HELP example_metric An example metric\n" +
                          "# TYPE example_metric gauge\n" +
                          "example_metric{label=\"value\"} 42\n";
            return Content(metrics, "text/plain");
        }
    }
}
