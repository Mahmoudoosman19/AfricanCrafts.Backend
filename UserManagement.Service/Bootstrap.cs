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
            services.AddScoped(typeof(IGenericRepository<>), typeof(UserGenericRepository<>));
            services.AddScoped<IUnitOfWork, UserUnitOfWork>();
            services.AddScoped<IAppleAuthService, AppleAuthService>();

            return services;
        }
    }
}
