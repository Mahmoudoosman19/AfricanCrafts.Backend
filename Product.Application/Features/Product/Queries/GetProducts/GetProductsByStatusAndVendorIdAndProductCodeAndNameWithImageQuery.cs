namespace Product.Application.Features.Product.Queries.GetProducts
{
    public sealed record GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery : IQuery<IReadOnlyList<ProductQueryResponse>>
    {
        public int PageSize { get; init; } = 20;
        public int PageIndex { get; init; } = 1;
        public bool? IsActive { get; init; }
        public string? Name { get; init; }
        public string? Size { get; init; }
        public int? Status { get; set; }
        public string? ProductCode { get; set; }

    }
}
