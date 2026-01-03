using Common.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Extensions
{
    public static class DbInterceptorsExtensions
    {
        public static void UpdateAuditableEntities(this DbContext dbContext)
        {
            var entries = dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                var now = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(e => e.CreatedOnUtc).CurrentValue = now;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    if (entityEntry.Properties.Any(p => p.IsModified && p.Metadata.Name != nameof(IAuditableEntity.CreatedOnUtc)))
                    {
                        entityEntry.Property(e => e.ModifiedOnUtc).CurrentValue = now;
                    }
                }
            }
        }
        public static void SoftDeleteEntities(this DbContext dbContext)
        {
            var entries = dbContext
                .ChangeTracker
                .Entries<ISoftDeleteEntity>()
                .Where(e => e.State == EntityState.Deleted || e.Property(e => e.IsDeleted).IsModified);

            foreach (var entityEntry in entries)
            {
                var timeEgy = DateTime.Now;

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry
                        .Property(e => e.IsDeleted).CurrentValue = true;
                    entityEntry
                        .Property(e => e.DeletedAt).CurrentValue = timeEgy;
                }
                else if (entityEntry.State == EntityState.Modified && entityEntry.Property(e => e.IsDeleted).IsModified)
                {
                    if (entityEntry.Property(e => e.IsDeleted).CurrentValue == false)
                    {
                        entityEntry
                            .Property(e => e.RestoredAt).CurrentValue = timeEgy;
                    }
                }
            }
        }
    }

}
