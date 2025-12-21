using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        // Add API service registrations here
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services
                .AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("OrderingDb")!);
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure middleware and endpoints here
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.MapHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                }
            );
            return app;
        }
    }
}
