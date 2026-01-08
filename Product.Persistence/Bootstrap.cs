using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Abstraction;
using Product.Persistence.Repositories;

namespace Product.Persistence
{
    public static class Bootstrap
    {
        public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
            services.AddScoped<IProductUnitOfWork, ProductUnitOfWork>();

            return services;
        }
    }
}
