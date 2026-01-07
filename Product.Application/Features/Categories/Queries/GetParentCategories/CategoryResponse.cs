namespace Product.Application.Features.Categories.Queries.GetParentCategories;

public sealed record CategoryResponse
{
    public Guid Id { get; set; }
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public Guid? SizeGroupId { get; set; }
    public bool IsActive { get; set; } = false;
    public string ImageUrl { get; set; } = null!;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
