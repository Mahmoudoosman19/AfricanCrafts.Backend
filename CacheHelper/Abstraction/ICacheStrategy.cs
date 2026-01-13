namespace CacheHelper.Abstraction;

public interface ICacheStrategy
{
    Task<T?> GetOrSetCacheDataAsync<T>(
            string cacheKey,
            Func<Task<T>>? initCache,
            DateTime? expiration = null,
            bool forceUpdate = false);
    bool ContainsKey(string key);
    void Remove(string key);
    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
}
