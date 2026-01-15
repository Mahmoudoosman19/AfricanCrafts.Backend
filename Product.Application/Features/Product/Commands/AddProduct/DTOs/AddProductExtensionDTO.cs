namespace Product.Application.Features.Product.Commands.AddProduct.DTOs
{
    public sealed record AddProductExtensionDTO
    {
        public Guid? SizeId { get; init; }
        public string ColorCode { get; init; } = null!;
        public int Amount { get; init; }
        public decimal Fees { get; init; }
    }
}
