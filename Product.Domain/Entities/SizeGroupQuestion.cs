using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities;

public class SizeGroupQuestion : Entity<Guid>, IAuditableEntity,ISoftDeleteEntity
{
    public string QuestionAr { get; private set; } = null!;
    public string QuestionEn { get; private set; } = null!;

    public Guid SizeGroupId { get; private set; }
    [ForeignKey(nameof(SizeGroupId))]
    public virtual SizeGroup? SizeGroup { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public bool IsDeleted {  get; set; }

    public DateTime? DeletedAt {  get; set; }

    public DateTime? RestoredAt {  get; set; }

    public void Update(SizeGroupQuestion question)
    {
        QuestionAr = question.QuestionAr;
        QuestionEn = question.QuestionEn;
    }

    public void SetSizeGroup(Guid sizeGroupId)
    {
        SizeGroupId = sizeGroupId;
    }
    public void Restored()
    {
        IsDeleted = false;   
    }
}