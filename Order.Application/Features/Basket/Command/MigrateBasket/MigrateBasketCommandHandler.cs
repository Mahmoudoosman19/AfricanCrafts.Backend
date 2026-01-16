using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.MigrateBasket
{
    internal sealed class MigrateBasketCommandHandler : ICommandHandler<MigrateBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ITokenExtractor _tokenExtractor;

        public MigrateBasketCommandHandler(
            IBasketRepository basketRepository,
            ITokenExtractor tokenExtractor)
        {
            _basketRepository = basketRepository;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel> Handle(MigrateBasketCommand request, CancellationToken cancellationToken)
        {
            var customerUser = _tokenExtractor.GetUserId();
            if (customerUser == Guid.Empty)
                return ResponseModel.Failure("User not found!");

            var guestBasket = await _basketRepository.GetBasketAsync(request.AnonymousId);
            if (guestBasket == null || !guestBasket.basketItems.Any())
                return ResponseModel.Success();

            var userBasket = await _basketRepository.GetBasketAsync(customerUser)
                             ?? new CustomerBasket(customerUser);

            userBasket.MergeBasket(guestBasket);

            await _basketRepository.UpdateBasketAsync(userBasket);

            await _basketRepository.DeleteBasketAsync(request.AnonymousId);

            return ResponseModel.Success();
        }
    }
}
