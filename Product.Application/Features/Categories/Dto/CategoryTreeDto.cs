namespace Product.Application.Features.Categories.Dto;

public sealed record CategoryTreeDto
{
    public Guid Id { get; init; }
    public Guid ParentId { get; init; }
    public string NameAr { get; init; }
    public string NameEn { get; init; }
    public string CategoryPath { get; init; }
    public string Image { get; init; }
    public string ImageFileId { get; init; }
    public Guid? SizeGroupId { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public DateTime? ModifiedOnUtc { get; init; }
    public List<CategoryTreeDto> SubCategories { get; set; } = new List<CategoryTreeDto>();
}

