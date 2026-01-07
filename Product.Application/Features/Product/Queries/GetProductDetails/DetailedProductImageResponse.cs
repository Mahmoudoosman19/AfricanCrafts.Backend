namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record DetailedProductImageResponse
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; init; } = null!;
        public string ColorCode { get; init; } = null!;
    }
}