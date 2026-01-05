using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations
{
    internal class NotificationMessagesConfiguration : IEntityTypeConfiguration<Domain.Entities.NotificationMessages>
    {
        public void Configure(EntityTypeBuilder<NotificationMessages> builder)
        {
            builder.ToTable(TableNames.NotificationMessages);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ResourceValue)
                .IsRequired();
            builder.Property(x => x.ResourceKey)
            .IsRequired();
            builder.Property(x => x.CurrentLanguage)
            .IsRequired();
        }
    }
}
