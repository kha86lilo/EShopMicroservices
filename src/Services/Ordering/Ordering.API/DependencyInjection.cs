namespace Ordering.API
{
    public static class DependencyInjection
    {
        // Add API service registrations here
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure middleware and endpoints here
            return app;
        }
    }
}
