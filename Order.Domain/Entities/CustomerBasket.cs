using Common.Domain.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class CustomerBasket : Entity<Guid>, IAuditableEntity
    {
        public Guid CustomerId { get; private set; }
        //[JsonProperty]

        //private readonly List<BasketItem> _items = new();
        public List<BasketItem> basketItems { get; private set; } = new();


        public CustomerBasket(Guid customerId) => CustomerId = customerId;

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        
       

        public void AddItem(Guid productId, Guid productExtensionId, string nameAr, string nameEn,
                            decimal unitPrice, int quantity, string colorCode, string? sizeName)
        {
            var existingItem = basketItems.FirstOrDefault(x => x.ProductId == productId
                                                       && x.ProductExtensionId == productExtensionId
                                                       && x.SelectedColorCode == colorCode);

            if (existingItem != null)
                existingItem.UpdateQuantity(quantity);
            else
                basketItems.Add(new BasketItem(productId, productExtensionId, nameAr, nameEn,
                                          unitPrice, quantity, colorCode, sizeName));
        }
    }
}
