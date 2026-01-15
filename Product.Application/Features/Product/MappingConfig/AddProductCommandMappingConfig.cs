using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using Mapster;
using Product.Application.Features.Product.Commands.AddProduct;
//using Product.Application.Features.Product.Commands.AddProduct.DTOs;
using Product.Application.Features.Product.Commands.SuperVisorAddProduct;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.MappingConfig
{
    internal class AddProductCommandMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddProductCommand, Domain.Entities.Product>()
                .ConstructUsing(src => new Domain.Entities.Product())
                .AfterMapping((src, dest) =>
                {
                    dest.SetName(src.NameAr, src.NameEn);
                    dest.SetDescription(src.DescriptionAr, src.DescriptionEn);
                    dest.SetPrice((decimal)src.Price);
                    dest.SetCategory(src.CategoryId);

                    // تعيين كود المنتج إذا كان قادماً في الـ Command
                    if (!string.IsNullOrEmpty(src.ProductCode))
                        dest.SetCode(src.ProductCode);

                    // تعيين اسم المجلد
                    dest.SetImagesFolderName(Guid.NewGuid());

                    // تحويل الـ Extensions (بيانات بسيطة)
                    if (src.Extensions != null)
                    {
                        foreach (var ext in src.Extensions)
                        {
                            var productExt = new ProductExtension();
                            productExt.SetSize(ext.SizeId ?? Guid.Empty);
                            productExt.SetColor(ext.ColorCode);
                            productExt.SetAmount(ext.Amount);
                            productExt.SetFees((decimal)(ext.Fees));
                            dest.AddExtension(productExt);
                        }
                    }

                });
        }
    }
}
