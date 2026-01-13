using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Abstraction;
using Order.Persistence.Repositories;

namespace Order.Persistence;

public static class Bootstrap
{
    public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
    {
        services.AddScoped(typeof(IOrderRepository<>), typeof(OrderRepository<>));
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
        services.AddScoped <IBasketRepository, BasketRepository>();

        return services;
    }
}
