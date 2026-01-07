//using CacheHelper.Abstraction;
//using Newtonsoft.Json;
//using StackExchange.Redis;

//namespace CacheHelper.Services
//{
//    public class RedisCacheService : ICacheStrategy
//    {
//        private readonly IConnectionMultiplexer _redisConnection;

//        public RedisCacheService(IConnectionMultiplexer redisConnection)
//        {
//            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
//        }

//        public async Task<T?> GetOrSetCacheDataAsync<T>(string key, T data, TimeSpan? expiry = null, bool forceUpdate = false)
//        {
//            var database = _redisConnection.GetDatabase();

//            if (!forceUpdate)
//            {
//                var cachedData = await database.StringGetAsync(key);

//                if (!cachedData.IsNullOrEmpty)
//                {
//                    return JsonConvert.DeserializeObject<T>(cachedData!);
//                }
//            }

//            if (data is not null)
//            {
//                await database.StringSetAsync(key, JsonConvert.SerializeObject(data), expiry);
//                return data;
//            }

//            return default;
//        }

//    }
//}
