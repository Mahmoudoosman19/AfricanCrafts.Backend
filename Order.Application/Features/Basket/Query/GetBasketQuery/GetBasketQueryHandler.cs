using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using Mapster;
using MapsterMapper;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketQuery
{
    internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketQueryResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<GetBasketQueryResponse>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.CustomerId);

            if (basket == null)
                return ResponseModel.Failure<GetBasketQueryResponse>(Messages.NotFound);

            var response = _mapper.Map<GetBasketQueryResponse>(basket);

            return ResponseModel.Success(response, 1);
        }
    }
}
