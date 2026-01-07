using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Product.Persistence.Repositories;

namespace Product.Persistence
{
    public static class Bootstrap
    {
        public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(ProductRepository<>));
            services.AddScoped<IUnitOfWork, ProductUnitOfWork>();

            return services;
        }
    }
}
