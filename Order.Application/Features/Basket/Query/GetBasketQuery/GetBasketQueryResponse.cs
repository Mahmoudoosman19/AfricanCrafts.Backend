using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketQuery
{
    public class GetBasketQueryResponse
    {
       //public Guid Id { get; set; }
        public Guid? CustomerId { get; init; }
        public List<BasketItemResponse> basketItems { get; init; }
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; init; }
    }
}
