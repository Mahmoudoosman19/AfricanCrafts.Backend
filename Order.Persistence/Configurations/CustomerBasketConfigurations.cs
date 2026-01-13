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
    internal class CustomerBasketConfigurations : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.ToTable(TableNames.CustomerBasket);
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.CustomerId).IsUnique();

            builder.HasMany(x => x.basketItems)
                   .WithOne()
                   .HasForeignKey(x => x.BasketId)
                   .OnDelete(DeleteBehavior.Cascade); 

            var navigation = builder.Metadata.FindNavigation(nameof(CustomerBasket.basketItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
