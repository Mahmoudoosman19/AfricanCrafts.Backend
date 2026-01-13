using CacheHelper.Abstraction;
using CacheHelper.Keys;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace CacheHelper.Services
{
    public class RedisCacheService : ICacheStrategy
    {
        private static ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<T?> GetOrSetCacheDataAsync<T>(
            string cacheKey,
            Func<Task<T>>? initCache,
            DateTime? expiration = null,
            bool forceUpdate = false)
        {
            if (string.IsNullOrEmpty(cacheKey))
                throw new ArgumentException("Cache key cannot be null or empty.", nameof(cacheKey));

            if (!forceUpdate)
            {
                var cachedData = await _database.StringGetAsync(cacheKey);
                if (cachedData.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(cachedData!);
                }
            }

            T result = default;
            if (initCache != null)
            {
                result = await initCache();
            }

            if (result != null)
            {
                var serializedData = JsonConvert.SerializeObject(result);

                TimeSpan? expiryTime = null;
                if (expiration.HasValue)
                {
                    expiryTime = expiration.Value - DateTime.Now;
                    if (expiryTime.Value.TotalSeconds <= 0) expiryTime = TimeSpan.FromMinutes(1);
                }

                await _database.StringSetAsync(cacheKey, serializedData,expiryTime,When.Always);
            }

            return result;
        }

        public bool ContainsKey(string key)
        {
            return _database.KeyExists(key);
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);

                var keys = server.Keys(pattern: prefixKey + "*");

                foreach (var key in keys)
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }
    }
}
