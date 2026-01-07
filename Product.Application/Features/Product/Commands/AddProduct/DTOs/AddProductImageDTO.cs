using Microsoft.AspNetCore.Http;

namespace Product.Application.Features.Product.Commands.AddProduct.DTOs
{
    public sealed record AddProductImageDTO
    {
        public IFormFile ImageFile { get; init; } = null!;
        public string ColorCode { get; init; } = null!;
    }
}
