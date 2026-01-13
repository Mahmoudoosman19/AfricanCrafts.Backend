using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketQuery
{
    public class GetBasketQuery : IQuery<GetBasketQueryResponse>
    {
        public Guid CustomerId {  get; set; }
    }
}
