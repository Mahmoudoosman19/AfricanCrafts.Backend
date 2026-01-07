using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using Product.Persistence.Constants;

namespace Product.Persistence.Configurations;

public class SizeGroupQuestionConfiguration : IEntityTypeConfiguration<SizeGroupQuestion>
{
    public void Configure(EntityTypeBuilder<SizeGroupQuestion> builder)
    {
        builder.ToTable(TableNames.SizeGroupQuestions);

        builder.HasKey(q => q.Id);

        builder.Property(s => s.QuestionAr);
        builder.Property(s => s.QuestionEn);

        builder.HasOne(s => s.SizeGroup)
               .WithMany(s => s.SizeGroupQuestions)
               .HasForeignKey(s => s.SizeGroupId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.QuestionAr);
        //.IsUnique();
        builder.HasIndex(s => s.QuestionEn);
               //.IsUnique();
    }
}