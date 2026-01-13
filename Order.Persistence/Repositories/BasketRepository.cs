using CacheHelper.Abstraction;
using Common.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ICacheStrategy _cache;
        private const string BasketKeyPrefix = "basket:";

        public BasketRepository(ICacheStrategy cache)
        {
            _cache = cache;
        }

        public async Task<CustomerBasket?> GetBasketAsync(Guid customerId)
        {
            var basket = await _cache.GetOrSetCacheDataAsync<CustomerBasket>(
                $"{BasketKeyPrefix}{customerId}", null);
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            await _cache.GetOrSetCacheDataAsync(
                $"{BasketKeyPrefix}{basket.CustomerId}",
                async () => basket,
                DateTime.Now.AddDays(30),
                forceUpdate: true
            );
            return basket;
        }

        public async Task<bool> DeleteBasketAsync(Guid customerId)
        {
            _cache.Remove($"{BasketKeyPrefix}{customerId}");
            return true;
        }
    }

}
