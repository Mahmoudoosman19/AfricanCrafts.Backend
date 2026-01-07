using Common.Domain.DomainEvents;

namespace Product.Domain.DomainEvents
{
    public sealed record ProductToggleActivationDomainEvent(Guid Id, Guid ProductId, bool IsActive) : DomainEvent(Id);
}
