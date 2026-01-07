using CacheHelper.Abstraction;
using Microsoft.Extensions.Caching.Memory;

namespace CacheHelper.Services;

public class MemoryCacheService : ICacheStrategy
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T?> GetOrSetCacheDataAsync<T>(
        string cacheKey,
        Func<Task<T>>? initCache,
        DateTime? expiration = null,
        bool forceUpdate = false)
    {
        if (string.IsNullOrEmpty(cacheKey))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(cacheKey));

        if (!forceUpdate && _memoryCache.TryGetValue(cacheKey, out T cachedValue))
        {
            return cachedValue;
        }

        T result = default;

        if (initCache != null)
        {
            result = await initCache();
        }

        if (result != null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                cacheEntryOptions.SetAbsoluteExpiration(expiration.Value);
            }

            _memoryCache.Set(
                cacheKey,
                result,
                cacheEntryOptions);
        }

        return result;
    }



    public bool ContainsKey(string key)
    {
        return _memoryCache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}
