using Common.Domain.Primitives;

namespace Order.Domain.Entities
{
    public class OrderItem : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Guid ProductExtensionId { get; private set; }

        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }
        public void SetOrderId(Guid orderId)
        {
            OrderId = orderId;
        }
        public void SetProductExtension(Guid productExtensionId)
        {
            ProductExtensionId = productExtensionId;
        }
        public void SetPrice(decimal price)
        {
            Price = price;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
      
    }

}
