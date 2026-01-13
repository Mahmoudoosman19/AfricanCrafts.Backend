using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MediatR;
using Order.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.DeleteBasket
{
    internal class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ResponseModel> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(request.CustomerId);
            if (!deleted)
            {
                return ResponseModel.Failure("Basket not found or already empty");
            }
            return ResponseModel.Success();
        }
    }
}
