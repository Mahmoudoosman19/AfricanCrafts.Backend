using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MediatR;
using Order.Application.Features.Basket.Query.GetBasketQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.UpdateBasket
{
    public class UpdateBasketCommand : ICommand<GetBasketQueryResponse>
    {
        public Guid CustomerId { get; set; }
        public List<BasketItemRequest> BasketItems { get; set; } = new();
    }

    public class BasketItemRequest
    {
        public Guid ProductId { get; set; }
        public Guid ProductExtensionId { get; set; }
        public string ProductNameAr { get; set; } = string.Empty;
        public string ProductNameEn { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string SelectedColorCode { get; set; } = string.Empty;
        public string? SelectedSizeName { get; set; }
    }
}
