using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.RemoveItemFromBasket
{
    public class RemoveItemFromBasketCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
