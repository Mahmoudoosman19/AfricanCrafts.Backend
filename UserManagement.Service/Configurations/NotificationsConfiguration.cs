using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations
{
    internal class NotificationsConfiguration : IEntityTypeConfiguration<Domain.Entities.Notifications>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Notifications> builder)
        {
            builder.ToTable(TableNames.Notifications);

            builder.HasKey(s => s.Id);

            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
