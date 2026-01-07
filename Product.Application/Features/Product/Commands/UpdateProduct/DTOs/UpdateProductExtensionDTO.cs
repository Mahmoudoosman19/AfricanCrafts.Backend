using Common.Application.Extensions.Mapster;

namespace Product.Application.Features.Product.Commands.UpdateProduct.DTOs
{
    public sealed class UpdateProductExtensionDTO : UpdateNestedListDto<Guid>
    {
        public Guid SizeId { get; init; }
        public string ColorCode { get; init; } = null!;
        public int Amount { get; init; }
        public decimal? Fees { get; init; }
        public bool IsDeleted { get; init; }
    }
}
