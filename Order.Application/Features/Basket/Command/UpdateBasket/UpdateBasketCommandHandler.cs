using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using Order.Application.Features.Basket.Query.GetBasketQuery;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.UpdateBasket
{
    internal class UpdateBasketCommandHandler : ICommandHandler<UpdateBasketCommand, GetBasketQueryResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<GetBasketQueryResponse>> Handle(
             UpdateBasketCommand request,
             CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.CustomerId);

            if (basket == null)
            {
                basket = new CustomerBasket(request.CustomerId);
            }

            foreach (var item in request.BasketItems)
            {
                basket.AddItem(
                    item.ProductId,
                    item.ProductExtensionId,
                    item.ProductNameAr,
                    item.ProductNameEn,
                    item.UnitPrice,
                    item.Quantity,
                    item.SelectedColorCode,
                    item.SelectedSizeName);
            }

            var result = await _basketRepository.UpdateBasketAsync(basket);

            if (result == null)
            {
                return ResponseModel.Failure<GetBasketQueryResponse>("Failed to update basket");
            }

            var response = _mapper.Map<GetBasketQueryResponse>(result);
            return ResponseModel.Success(response);
        }
    }
}
