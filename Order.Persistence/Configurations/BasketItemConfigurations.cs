using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.Configurations
{
    internal class BasketItemConfigurations : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable(TableNames.BasketItem);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductNameAr)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.ProductNameEn)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.SelectedColorCode)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.SelectedSizeName)
                   .HasMaxLength(50);

            builder.Property(x => x.Quantity)
                   .IsRequired();
        }
    }
}
