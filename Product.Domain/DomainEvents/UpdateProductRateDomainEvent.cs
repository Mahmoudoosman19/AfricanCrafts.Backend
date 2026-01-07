using Common.Domain.DomainEvents;

namespace Product.Domain.DomainEvents
{
    public sealed record UpdateProductRateDomainEvent(Guid Id, Guid ProductId, double Rate) : DomainEvent(Id);
}
