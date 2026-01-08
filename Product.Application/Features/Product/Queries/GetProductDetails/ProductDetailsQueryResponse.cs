using Product.Application.Features.Categories.Queries.GetParentCategories;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record ProductDetailsQueryResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; init; } = null!;
        public string NameEn { get; init; } = null!;
        public string DescriptionAr { get; init; } = null!;
        public string DescriptionEn { get; init; } = null!;
        public decimal Price { get; init; }
        public decimal DiscountPrice { get; init; }
        public bool IsActive { get; init; }
        public double Rate { get; set; }
        public int NumberOfRatings { get; set; }
        public Guid CreatedBy { get; init; }
        public Guid VendorId { get; init; }
        public string? ProductCode { get; init; }
        public CategoryResponse Category { get; init; } = null!;
        public List<DetailedProductImageResponse> Images { get; init; } = new();
        public List<ProductExtensionResponse> Extensions { get; init; } = new();
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; init; }
    }
}
