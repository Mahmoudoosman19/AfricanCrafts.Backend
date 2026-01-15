using Microsoft.AspNetCore.Http;
using Product.Application.Features.Product.Commands.AddProduct.DTOs;
using System.ComponentModel;

namespace Product.Application.Features.Product.Commands.AddProduct
{
    public sealed record AddProductCommand : ICommand
    {
        public string NameAr { get; init; } = null!;
        public string NameEn { get; init; } = null!;
        public string DescriptionAr { get; init; } = null!;
        public string DescriptionEn { get; init; } = null!;
        public string? ProductCode { get; set; } 
        public decimal Price { get; init; }
        public Guid CategoryId { get; init; }

        public List<AddProductImageDTO> Images { get; init; } = [];

        public List<AddProductExtensionDTO> Extensions { get; init; } = [];
    }

    
}
