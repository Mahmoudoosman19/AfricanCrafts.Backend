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


        public CustomerBasket(Guid customerId)
        {
            CustomerId = customerId;
            CreatedOnUtc = DateTime.UtcNow;
        }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }



        public void AddItem(Guid productId, Guid productExtensionId, string nameAr, string nameEn,
                            decimal unitPrice, int quantity, string colorCode, string? sizeName)
        {
            var existingItem = basketItems.FirstOrDefault(x => x.ProductId == productId
                                                       && x.ProductExtensionId == productExtensionId
                                                       && x.SelectedColorCode == colorCode);

            if (existingItem != null)
                existingItem.AddQuantity(quantity); // increase quantity if product already exist in basket
            else
                basketItems.Add(new BasketItem(productId, productExtensionId, nameAr, nameEn,
                                          unitPrice, quantity, colorCode, sizeName));
        }
        public void RemoveItem(Guid productId)
        {
            var item = basketItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                basketItems.Remove(item);
            }
        }

        public void UpdateQuantity(Guid productId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                RemoveItem(productId);
                return;
            }

            var item = basketItems.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.SetQuantity(newQuantity);
            }
        }
        public void MergeBasket(CustomerBasket guestBasket)
        {
            foreach (var item in guestBasket.basketItems)
            {
                this.AddItem(
                    item.ProductId,
                    item.ProductExtensionId,
                    item.ProductNameAr,
                    item.ProductNameEn,
                    item.UnitPrice,
                    item.Quantity,
                    item.SelectedColorCode,
                    item.SelectedSizeName);
            }
        }
    }
}
