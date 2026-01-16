using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Checkout.Command.CheckoutOrder
{
    public class CheckoutOrderCommand : ICommand
    {
        public long PaymentMethodId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? City { get; set; }
    }
}
