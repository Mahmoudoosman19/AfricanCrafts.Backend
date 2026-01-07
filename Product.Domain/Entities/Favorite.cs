using Common.Domain.Primitives;

namespace Product.Domain.Entities
{
    public class Favorite : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public virtual Product Product { get; private set; }
        public Guid CustomerId { get; private set; }

        public Favorite()
        {

        }

        public Favorite(Guid productId, Guid customerId)
        {
            ProductId = productId;
            CustomerId = customerId;
        }
    }
}
