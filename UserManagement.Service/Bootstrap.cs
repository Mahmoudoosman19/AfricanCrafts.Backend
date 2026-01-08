using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Abstraction;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Persistence
{
    public static class Bootstrap
    {
        public static IServiceCollection AddPersistenceStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
            services.AddScoped<IAppleAuthService, AppleAuthService>();

            return services;
        }
    }
}
