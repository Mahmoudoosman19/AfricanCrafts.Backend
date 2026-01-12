using Common.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Persistence;

namespace Order.Presentation.Configurations
{
    public static class DbConfig
    {
        public static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default")!;

            services.AddSingleton<DBSavingChangesInterceptor>();

            services.AddDbContext<OrderDbContext>(
                (sp, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(connectionString);
                    optionsBuilder.AddInterceptors(sp.GetRequiredService<DBSavingChangesInterceptor>());
                });

            return services;
        }
    }
}
