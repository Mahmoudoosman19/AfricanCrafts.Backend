using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Query.GetBasketQuery
{
    public class BasketItemResponse
    {
        public Guid ProductId { get; init; }
        public Guid ProductExtensionId { get; init; }
        public string ProductNameAr { get; init; } = null!;
        public string ProductNameEn { get; init; } = null!;
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public string SelectedColorCode { get; init; } = null!;
        public string? SelectedSizeName { get; init; }
    }
}
