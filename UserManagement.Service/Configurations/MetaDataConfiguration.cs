using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Persistence.Constants;

namespace UserManagement.Persistence.Configurations
{
    internal class MetaDataConfiguration : IEntityTypeConfiguration<MetaData>
    {
        public void Configure(EntityTypeBuilder<MetaData> builder)
        {
            builder.ToTable(TableNames.MetaData);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApplicationRate)
                .IsRequired()
                .HasPrecision(18, 2); 
        }
    }
}
