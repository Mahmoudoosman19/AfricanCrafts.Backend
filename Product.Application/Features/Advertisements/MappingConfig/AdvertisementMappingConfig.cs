using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Advertisements.Queries.CustomerGetAllAdvertisment;
using Product.Application.Features.Advertisements.Queries.GetList;

namespace Product.Application.Features.Advertisements.MappingConfig
{
    internal class AdvertisementMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Advertisement, AdvertisementQueryResponse>()
              .Map(dest => dest.ImageName, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Advertisement));
           
            config.NewConfig<Domain.Entities.Advertisement, CustomerAdvertisementsQueryResponse>()
              .Map(dest => dest.ImageName, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Advertisement));
        }
    }
}
