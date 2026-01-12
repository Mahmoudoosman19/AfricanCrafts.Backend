using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Order.Persistence.Repositories;

namespace Order.Persistence;

public static class Bootstrap
{
    public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(OrderRepository<>));
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

        return services;
    }
}
