using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.DeleteBasket
{
    public class DeleteBasketCommand : ICommand
    {
        public Guid CustomerId { get; set; }
    };
}
