using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Seeders;
using UserManagement.Persistence;
using UserManagement.Presentation.Configurations;
using UserManagement.Presentation.Extensions;
using UserManagement.Presentation.OptionsSetup;

namespace UserManagement.Presentation
{
    public static class Bootstrap
    {
        public static IServiceCollection AddUserManagerStrapping(this IServiceCollection services,IConfiguration configuration)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<OTPOptionsSetup>();

            services.AddDbConfig(configuration);
            services.AddAppServicesDIConfig();

            services.AddApplicationStrapping();
            services.AddPersistenceStrapping();
            services.AddInfrastructureStrapping();

            services.AddIdentityServices(configuration);
            services.AddDBSeederExtension();

            return services;
        }
    }
}
