using Mikrotik.Exporter.Services;
using Mikrotik.Exporter.Providers;
using Mikrotik.Exporter.HealthChecks;
using EasyExtensions.AspNetCore.Extensions;

namespace Mikrotik.Exporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services
                .AddCpuUsageService()
                .AddHealthChecks()
                .AddCheck<RemoteDeviceHealthCheck>("Remote Device").Services
                .AddSingleton<MikrotikMetricsProvider>()
                .AddSingleton<ScrapMetricsHostedService>()
                .AddSingleton<MikrotikConfigurationProvider>()
                .AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>())
                .AddHostedService(provider => provider.GetRequiredService<ScrapMetricsHostedService>());

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
