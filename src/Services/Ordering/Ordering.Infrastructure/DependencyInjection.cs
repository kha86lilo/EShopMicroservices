using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("OrderingConnectionString");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<AppDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(connectionString);
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                }
            );
            services.AddScoped<IApplicationDbContext, AppDbContext>();
            return services;
        }
    }
}
