using Common.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class BasketItem : Entity<Guid>
    {
        public Guid BasketId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid ProductExtensionId { get; private set; }
        public string ProductNameAr { get; private set; }
        public string ProductNameEn { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string SelectedColorCode { get; private set; }
        public string? SelectedSizeName { get; private set; }

        private BasketItem() { }
      
        public BasketItem(Guid productId, Guid productExtensionId, string productNameAr, string productNameEn,
                          decimal unitPrice, int quantity, string selectedColorCode, string? selectedSizeName)
        {
            ProductId = productId;
            ProductExtensionId = productExtensionId;
            ProductNameAr = productNameAr;
            ProductNameEn = productNameEn;
            UnitPrice = unitPrice;
            Quantity = quantity;
            SelectedColorCode = selectedColorCode;
            SelectedSizeName = selectedSizeName;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            Quantity = quantity;
        }
        public void AddQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            Quantity += quantity;
        }
    }
}
