using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstractions;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Persistence
{
    public static class Bootstrap
    {
        public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppleAuthService, AppleAuthService>();

            return services;
        }
    }
}
