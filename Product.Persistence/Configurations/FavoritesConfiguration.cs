using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal class FavoritesConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable(TableNames.Favorites);

            builder.HasKey(x => x.Id);

            builder.Property(f => f.ProductId)
                   .IsRequired();

            builder.Property(f => f.CustomerId)
                   .IsRequired();

            builder.HasOne(f => f.Product)
                   .WithMany()
                   .HasForeignKey(f => f.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
