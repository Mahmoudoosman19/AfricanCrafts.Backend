using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using System.ComponentModel;

namespace Product.Application.Features.Product.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand : ICommand<string>
    {
        public Guid Id { get; init; }
        [DisplayName("الاسم العربي")]
        public string NameAr { get; init; } = null!;
        [DisplayName("الاسم الانجليزي")]
        public string NameEn { get; init; } = null!;
        [DisplayName("الوصف العربي")]
        public string DescriptionAr { get; init; } = null!;
        [DisplayName("الوصف الانجليزي")]
        public string DescriptionEn { get; init; } = null!;
        [DisplayName("السعر")]
        public decimal Price { get; init; }
        [DisplayName("الفئة")]
        public Guid CategoryId { get; init; }
        public Guid? PointsId { get; init; }
        [DisplayName("الصور")]
        public List<UpdateProductImageDTO> Images { get; init; } = new();
        [DisplayName("الاحجام")]
        public List<UpdateProductExtensionDTO> Extensions { get; init; } = new();
    }
}
