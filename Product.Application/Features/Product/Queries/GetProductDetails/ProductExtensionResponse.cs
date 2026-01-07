using Product.Application.Features.SizeGroups.Queries.GetSizeGroupById;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    public sealed record ProductExtensionResponse
    {
        public Guid Id { get; set; }
        public string ColorCode { get; init; } = null!;
        public SizeResponse Size { get; set; }
        public int Amount { get; private set; }
        public decimal Fees { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
