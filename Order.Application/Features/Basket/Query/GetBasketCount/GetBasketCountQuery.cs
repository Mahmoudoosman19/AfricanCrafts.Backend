using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketCount
{
    public class GetBasketCountQuery: IQuery<int>
    {
        public Guid CustomerId { get; set; }
    }
}
