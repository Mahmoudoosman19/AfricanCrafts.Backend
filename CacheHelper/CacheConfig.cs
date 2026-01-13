using CacheHelper.Abstraction;
using CacheHelper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
namespace CacheHelper
{
    public static class CacheConfig
    {
        public static IServiceCollection AddCacheBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddScoped<CacheContext>();

            var useRedisStr = configuration["CacheSettings:UseRedis"];
            bool.TryParse(useRedisStr, out bool useRedis);
            if (useRedis)
            {
                services.AddRedisConfig(configuration);
            }
            else
            {
                services.AddMemoryCacheStrategy();
            }

            return services;
        }

        public static IServiceCollection AddRedisConfig(this IServiceCollection services, IConfiguration config)
        {
            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { { config["RedisConfig:Host"] ?? "localhost", int.Parse(config["RedisConfig:Port"] ?? "6379") } },

                Ssl = false,

                AbortOnConnectFail = false,

                ConnectTimeout = 5000,
                SyncTimeout = 5000
            };

            if (!string.IsNullOrEmpty(config["RedisConfig:Password"]))
            {
                options.Password = config["RedisConfig:Password"];
            }

            var redisConnection = ConnectionMultiplexer.Connect(options);
            services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            services.AddSingleton<ICacheStrategy, RedisCacheService>();

            return services;
        }

        public static IServiceCollection AddMemoryCacheStrategy(this IServiceCollection services)
        {
            services.AddSingleton<ICacheStrategy, MemoryCacheService>();
            return services;
        }
    }
}


