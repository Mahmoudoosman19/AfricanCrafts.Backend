using Product.Domain.Enums;

namespace Product.Application.Features.Product.Queries.GetProducts
{
    public sealed class ProductQueryResponse
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
        public List<ProductImageResponse> Images { get; init; } = new();
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; init; }
        public ProductStatus ViewStatus { get;  set; }
    }
}
