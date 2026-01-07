namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record ColorResponse
    {
        public Guid Id { get; set; }
        public string NameAr { get; init; } = null!;
        public string NameEn { get; init; } = null!;
        public string Code { get; init; } = null!;
    }
}
