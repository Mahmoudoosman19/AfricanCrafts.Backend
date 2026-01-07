using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(TableNames.Reviews);

            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(c => c.Rate)
                   .IsRequired()
                   .HasDefaultValue(2.5);

            builder.Property(r => r.Comment)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedOnUtc)
                .IsRequired();

            builder.Property(r => r.ModifiedOnUtc);

            builder.HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
