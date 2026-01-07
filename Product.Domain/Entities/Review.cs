using Common.Domain.Primitives;
using Product.Domain.DomainEvents;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Review : AggregateRoot<Guid>, IAuditableEntity
    {
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        [Range(0.0, 5.0)]
        public double Rate { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public Review()
        {

        }

        public Review(Guid userId, Guid productId, double rate, string? comment)
        {
            UserId = userId;
            ProductId = productId;
            Rate = rate;
            Comment = comment;
        }
        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
        public void SetRate(double rate)
        {
            Rate = rate;
        }
        public void SetComment(string? comment)
        {
            Comment = comment;
        }

        public void RaiseRateUpdatesDomainEvents(Guid vendorId)
        {
            RaiseDomainEvent(new UpdateProductRateDomainEvent(Guid.NewGuid(), ProductId, Rate));

            RaiseDomainEvent(new UpdateVendorRateDomainEvent(Guid.NewGuid(), vendorId, Rate));
        }
    }
}
