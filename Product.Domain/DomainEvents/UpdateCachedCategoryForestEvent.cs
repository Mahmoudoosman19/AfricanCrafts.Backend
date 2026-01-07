using Common.Domain.DomainEvents;

namespace Product.Domain.DomainEvents;

public sealed record UpdateCachedCategoryForestEvent(
    Guid Id,
    Guid categoryId) : DomainEvent(Id);
