using Common.Domain.DomainEvents;

namespace Product.Domain.DomainEvents
{
    public sealed record UpdateVendorRateDomainEvent(Guid Id, Guid VendorId, double Rate) : DomainEvent(Id);
}
