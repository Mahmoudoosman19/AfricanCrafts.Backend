using Product.Application.Features.Product.Queries.GetProductDetails;

namespace Product.Application.Features.Product.Queries.GetProductDetailsCustomer
{
    public class CustomerProductDetailsQueryResponse
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
        public decimal DiscountPrice { get; init; }
        public double Rate { get; set; }
        public string? ProductCode { get; init; }
        public List<ProductImageResponse> Images { get; init; } = new();
    }
}
