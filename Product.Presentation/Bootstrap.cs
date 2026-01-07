using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application;
using Product.Infrastructure;
using Product.Persistence;
using Product.Presentation.Configurations;

namespace Product.Presentation
{
    public static class Bootstrap
    {
        public static IServiceCollection AddProductStrapping (this IServiceCollection services, IConfiguration configuration)
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
