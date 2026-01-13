using Common.Domain.Repositories;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Abstraction
{
    public interface IBasketRepository 
    {
        Task<CustomerBasket?> GetBasketAsync(Guid customerId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(Guid customerId);
    }
}
