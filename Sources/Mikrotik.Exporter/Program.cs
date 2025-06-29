using Mikrotik.Exporter.Services;
using Mikrotik.Exporter.HealthChecks;

namespace Mikrotik.Exporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services
                .AddHealthChecks()
                .AddCheck<RemoteDeviceHealthCheck>("Remote Device").Services
                .AddHostedService<ScrapMetricsHostedService>()
                .AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());

            var app = builder.Build();
            app.UseAuthentication()
                .UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.MapHealthChecks("/healthz");
            app.Run();
        }
    }
}
