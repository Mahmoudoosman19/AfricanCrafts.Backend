using Common.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserManagement.Persistence;

namespace UserManagement.Presentation.Configurations
{
    public static class DbConfig
    {
        public static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default")!;

            services.AddSingleton<DBSavingChangesInterceptor>();

            services.AddDbContext<UserManagementDbContext>(
                (sp, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(connectionString);
                    optionsBuilder.EnableSensitiveDataLogging();
                    optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
                    optionsBuilder.AddInterceptors(sp.GetRequiredService<DBSavingChangesInterceptor>());
                });

            return services;
        }
    }
}
