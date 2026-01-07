using System.ComponentModel;
using Product.Application.Features.Product.Commands.AddProduct.DTOs;

namespace Product.Application.Features.Product.Commands.SuperVisorAddProduct;

public class SuperVisorAddProductCommand : ICommand<string>
{
    [DisplayName("معرف البائع")]
    public Guid VendorId { get; init; }
    [DisplayName("الاسم العربي")]
    public string NameAr { get; init; } = null!;
    [DisplayName("الاسم الانجليزي")]
    public string NameEn { get; init; } = null!;
    [DisplayName("الوصف العربي")]
    public string DescriptionAr { get; init; } = null!;
    [DisplayName("الوصف الانجليزي")]
    public string DescriptionEn { get; init; } = null!;
    [DisplayName("الباركود")]
    public string? ProductCode { get; set; }
    [DisplayName("السعر")]
    public double Price { get; init; }
    [DisplayName("الفئة")]
    public Guid CategoryId { get; init; }
    public Guid? PointsId { get; init; }
    [DisplayName("الصور")]
    public List<AddProductImageDTO> Images { get; init; } = [];
    [DisplayName("الاحجام")]
    public List<AddProductExtensionDTO> Extensions { get; init; } = [];
}