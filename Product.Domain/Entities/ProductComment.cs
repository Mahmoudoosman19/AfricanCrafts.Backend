using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities;

public class ProductComment : Entity<Guid>, IAuditableEntity,ISoftDeleteEntity
{
    public Guid ProductId { get; private set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; private set; } = null!;

    public string Comment { get; private set; } = null!;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? RestoredAt { get; set; }

    public ProductComment()
    {

    }

    public ProductComment(string comment, Guid createdBy)
    {
        Comment = comment;
        CreatedBy = createdBy;
    }
    public void Restored()
    {
        IsDeleted=false;    
    }
}