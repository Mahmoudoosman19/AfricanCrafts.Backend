namespace Bus.Contracts.Models.Product;

public sealed record CategoryModel
{
    public Guid Id { get; set; }
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public bool IsActive { get; set; } = false;
    public string ImageUrl { get; set; } = null!;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
