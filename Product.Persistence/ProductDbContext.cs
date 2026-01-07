using Common.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Product.Persistence;

public sealed class ProductDbContext : DbContext
{
    private readonly IPublisher _publisher;


    public ProductDbContext(DbContextOptions<ProductDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("product");
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> domainEvents = new();
        var entities = ChangeTracker.Entries().Select(e => e.Entity).ToList();

        foreach (var entity in entities)
        {
            var getDomainEventsMethod = entity.GetType().GetMethod("GetDomainEvents");
            var clearMethod = entity.GetType().GetMethod("ClearDomainEvents");

            if (getDomainEventsMethod is null || clearMethod is null)
                continue;

            var events = getDomainEventsMethod.Invoke(entity, null)!;

            domainEvents.AddRange((List<IDomainEvent>)events);

            clearMethod.Invoke(entity, null);
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        try
        {
            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);
        }
        catch (Exception ex)
        {

        }


        return result;
    }
}
