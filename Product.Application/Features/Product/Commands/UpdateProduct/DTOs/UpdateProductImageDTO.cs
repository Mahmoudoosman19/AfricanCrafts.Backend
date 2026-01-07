using Common.Application.Extensions.Mapster;
using Microsoft.AspNetCore.Http;

namespace Product.Application.Features.Product.Commands.UpdateProduct.DTOs
{
    public sealed class UpdateProductImageDTO : UpdateNestedListDto<Guid>
    {
        public IFormFile? ImageFile { get; init; }
        public string ColorCode { get; init; } = null!;
    }
}
