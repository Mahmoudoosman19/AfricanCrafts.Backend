using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using Order.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketCount
{
    internal sealed class GetBasketCountQueryHandler : IQueryHandler<GetBasketCountQuery, int>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketCountQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ResponseModel<int>> Handle(GetBasketCountQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.CustomerId);

            if (basket == null)
            {
                return ResponseModel.Success(0); 
            }

            var totalCount = basket.basketItems.Sum(x => x.Quantity);

            return ResponseModel.Success(totalCount);
        }
    }
}
