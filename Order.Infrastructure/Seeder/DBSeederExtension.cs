using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstraction;

namespace Order.Infrastructure.Seeder
{
    public static class DBSeederExtension
    {
        public static async Task<IServiceCollection> AddDBSeederExtension(this IServiceCollection services)
        {
            services.AddScoped<ISeeder, PaymentMethodSeeder>();
            services.AddScoped<ISeeder, OrderStatusSeeder>();
            services.AddScoped<ISeeder, PaymentStatusSeeder>();

            using var serviceProvider = services.BuildServiceProvider();

            var seeders = serviceProvider.GetServices<ISeeder>();

            seeders = seeders.OrderBy(x => x.ExecutionOrder);

            foreach (var seeder in seeders)
                await seeder.SeedAsync();

            return services;
        }
    }
}
