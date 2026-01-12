using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application;
using Order.Infrastructure;
using Order.Persistence;
using Order.Presentation.Configurations;

namespace Order.Presentation
{
    public static class Bootstrap
    {
        public static IServiceCollection AddOrderStrapping (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbConfig(configuration);
            services.AddAppServicesDIConfig();

            // layers regestration
            services.AddApplicationStrapping();
            services.AddPersistenceStrapping();
            services.AddInfrastructureStrapping();


            return services;
        }
    }
}
