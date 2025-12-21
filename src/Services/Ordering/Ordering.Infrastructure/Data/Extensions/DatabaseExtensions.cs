using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await SeedAsync(context);
    }

    private static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(InitialData.GetCustomers());
            await context.SaveChangesAsync();
        }
        if (!context.Products.Any())
        {
            context.Products.AddRange(InitialData.GetProducts());
            await context.SaveChangesAsync();
        }
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(InitialData.GetOrders());
            await context.SaveChangesAsync();
        }
        if (!context.OrderItems.Any())
        {
            context.OrderItems.AddRange(InitialData.GetOrderItems());
            await context.SaveChangesAsync();
        }
    }
}
