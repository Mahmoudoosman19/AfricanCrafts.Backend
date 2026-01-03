using Common.Domain.Primitives;

namespace Common.Domain.DomainEvents
{
    public abstract record DomainEvent(Guid Id) : IDomainEvent;
}
