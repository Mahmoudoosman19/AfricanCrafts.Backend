using Mapster;
using Order.Application.Features.Basket.Query.GetBasketQuery;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.MappingConfig
{
    internal class GetBasketMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<CustomerBasket, GetBasketQueryResponse>()
            //    .Map(dest => dest.CustomerId, src => src.Id);
        }
    }
}
