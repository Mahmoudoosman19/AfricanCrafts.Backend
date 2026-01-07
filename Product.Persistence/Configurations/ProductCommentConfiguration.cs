using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations;

internal sealed class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
{
    public void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        builder.ToTable(TableNames.ProductComments);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Comment).HasMaxLength(500);
        
        builder.HasOne(c => c.Product)
            .WithMany(p => p.Comments)
            .HasForeignKey(pe => pe.ProductId);
    }
}