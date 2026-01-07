using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Product.Queries.GetProductByVendorId;

namespace Product.Application.Features.Product.MappingConfig
{
    public class GetProductByVendorIdMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Product, GetProductQueryResponse>()
                .Map(dest => dest.ProductPrice, src => src.Price)
                .Map(dest => dest.ProductRate, src => src.Rate)
                .Map(dest => dest.ProductId, src => src.Id)
                .Map(dest => dest.ProductNameAr, src => src.NameAr)
                .Map(dest => dest.ProductNameEn, src => src.NameEn)
                              .Map(dest => dest.Images,
                       (src => src.Images.Select(img => ImageKitBaseUrl.GenerateImageUrl(img.ImageName, FileType.Product))));
            ;


        }
    }

}


