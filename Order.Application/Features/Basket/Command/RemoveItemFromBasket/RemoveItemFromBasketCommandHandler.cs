using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using Order.Application.Specifications.Basket;
using Order.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.RemoveItemFromBasket
{
    internal class RemoveItemFromBasketCommandHandler : ICommandHandler<RemoveItemFromBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public RemoveItemFromBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ResponseModel> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.CustomerId);
            if (basket == null)
                return ResponseModel.Failure("Basket Not Found");

            basket.RemoveItem(request.ProductId);

            await _basketRepository.UpdateBasketAsync(basket);

            return ResponseModel.Success();
        }
    }
}
