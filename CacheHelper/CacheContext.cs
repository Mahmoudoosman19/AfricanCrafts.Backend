using CacheHelper.Abstraction;

namespace CacheHelper
{
    public class CacheContext
    {
        private ICacheStrategy _cacheStrategy;

        public CacheContext(ICacheStrategy cacheStrategy)
        {
            _cacheStrategy = cacheStrategy;
        }

        public void SetStrategy(ICacheStrategy cacheStrategy)
        {
            _cacheStrategy = cacheStrategy;
        }

        public async Task<T?> GetOrSetCacheDataAsync<T>(
            string cacheKey,
            Func<Task<T>>? initCache,
            DateTime? expiration = null,
            bool forceUpdate = false)
        {
            return await _cacheStrategy.GetOrSetCacheDataAsync(cacheKey, initCache, expiration, forceUpdate);
        }

        public bool ContainsKey(string key)
        {
            return _cacheStrategy.ContainsKey(key);
        }

        public void Remove(string key)
        {
            _cacheStrategy.Remove(key);
        }

    }
}
