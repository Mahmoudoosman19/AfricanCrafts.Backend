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
    internal class RefundProofConfiguration : IEntityTypeConfiguration<RefundProof> 
    {

        public void Configure(EntityTypeBuilder<RefundProof> builder)
        {
            builder.ToTable(TableNames.RefundProof);

            builder.HasKey(p => p.Id);



            builder.Property(p => p.RefundProofImgId)
                .IsRequired();

            builder.Property(p => p.RefundProofImgUrl)
              .IsRequired();
        }
    }
}
