namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record ProductImageResponse
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string ColorCode { get; init; } = null!;
    }
}