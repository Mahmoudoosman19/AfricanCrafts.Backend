using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using Order.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.UpdateItemQuantity
{
    internal sealed class UpdateItemQuantityCommandHandler : ICommandHandler<UpdateItemQuantityCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public UpdateItemQuantityCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ResponseModel> Handle(UpdateItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.CustomerId);
            if (basket == null) return ResponseModel.Failure("Basket Not Found");

            basket.UpdateQuantity(request.ProductId, request.Quantity);

            await _basketRepository.UpdateBasketAsync(basket);

            return ResponseModel.Success();
        }
    }
}
