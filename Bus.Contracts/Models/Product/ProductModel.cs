namespace Bus.Contracts.Models.Product;

public sealed record ProductModel
{
    public Guid Id { get; set; }
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string DescriptionAr { get; set; } = null!;
    public string DescriptionEn { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public bool IsActive { get; set; }
    public double Rate { get; set; }
    public int NumberOfRatings { get; set; }
    public CategoryModel Category { get; set; } = null!;
    public List<ProductImageModel> Images { get; set; } = new();
    public List<ProductExtensionModel> Extensions { get; set; } = new();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid ImagesFolderName { get; set; }

}
