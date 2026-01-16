using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Enum;
using Order.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Checkout.Command.CheckoutOrder
{
    internal class CheckoutOrderCommandHandler : ICommandHandler<CheckoutOrderCommand>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository<Domain.Entities.Order> _orderRepository; 
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly ITokenExtractor _tokenExtractor;

        public CheckoutOrderCommandHandler(
        IBasketRepository basketRepository,
        IOrderRepository<Domain.Entities.Order> orderRepository,
        IOrderUnitOfWork unitOfWork,
        ITokenExtractor tokenExtractor)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var userCustomer = _tokenExtractor.GetUserId();
            if (userCustomer == Guid.Empty)
                return ResponseModel.Failure("User not exist!");

            var basket = await _basketRepository.GetBasketAsync(userCustomer);

            if (basket == null || !basket.basketItems.Any())
            {
                return ResponseModel.Failure(Messages.NotFound);
            }

            // create order
            var order = new Domain.Entities.Order();

            order.SetCustomerId(userCustomer);
            order.SetOrderNumber($"ORD-{DateTime.UtcNow.Ticks}"); 
            order.SetStatus(OrderStatusEnum.Pending); 
            order.SetPaymentStatus(PaymentStatusEnum.Unpaid); 
            order.SetPaymentMethod((PaymentMethodEnum)request.PaymentMethodId);
            
            decimal totalOrderPrice = 0;

            // basket items => order items
            foreach (var basketItem in basket.basketItems)
            {
                var orderItem = new OrderItem();

                orderItem.SetProductId(basketItem.ProductId);
                orderItem.SetProductExtension(basketItem.ProductExtensionId);
                orderItem.SetPrice(basketItem.UnitPrice);
                orderItem.SetQuantity(basketItem.Quantity);
                orderItem.SetOrderId(order.Id);

                
                order.AddItem(orderItem);

                totalOrderPrice += (basketItem.UnitPrice * basketItem.Quantity);
            }

            order.SetPrice(totalOrderPrice, 0); // 0 for discount

            await _orderRepository.AddAsync(order);
            var success = await _unitOfWork.CompleteAsync(cancellationToken) > 0;

            if (!success)
            {
                return ResponseModel.Failure(Messages.IncorrectData);
            }

            await _basketRepository.DeleteBasketAsync(userCustomer);

            return ResponseModel.Success(order.Id);
        }
    }
}
