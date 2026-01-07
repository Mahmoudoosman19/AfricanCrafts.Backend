using CacheHelper.Abstraction;
using CacheHelper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CacheHelper
{
    public static class CacheConfig
    {
        public static IServiceCollection AddCacheHelperDI(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheStrategy, MemoryCacheService>();
            services.AddScoped<CacheContext>();

            return services;
        }

        public static IServiceCollection AddRedisConfig(this IServiceCollection services, IConfiguration config)
        {
            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { { config["RedisConfig:Host"]!, int.Parse(config["RedisConfig:Port"]!) } },
                User = config["RedisConfig:UserName"],
                Password = config["RedisConfig:Password"],
                AbortOnConnectFail = false,
                Ssl = true,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };

            var redisConnection = ConnectionMultiplexer.Connect(options);
            IDatabase db = redisConnection.GetDatabase();

            services.AddSingleton<IConnectionMultiplexer>(redisConnection);

            return services;
        }
    }
}


