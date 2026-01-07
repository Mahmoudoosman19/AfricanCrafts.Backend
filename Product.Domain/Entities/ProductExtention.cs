using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductExtension : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; private set; }

        public Guid? SizeId { get; private set; }
        [ForeignKey(nameof(SizeId))]
        public virtual Size? Size { get; private set; }

        public string ColorCode { get; private set; } = null!;
        public int Amount { get; private set; }
        public decimal? Fees { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public void SetProduct(Guid productId)
        {
            ProductId = productId;
        }

        public void SetSize(Guid sizeId)
        {
            SizeId = sizeId;
        }

        public void SetColor(string colorCode)
        {
            ColorCode = colorCode;
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }

        public void SetFees(decimal fees)
        {
            Fees = fees;
        }
    }
}
