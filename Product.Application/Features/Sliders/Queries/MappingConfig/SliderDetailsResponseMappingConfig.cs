using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Sliders.Queries.GetListSlider;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Queries.MappingConfig
{
    internal class SliderDetailsResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Slider, SliderResponse>()
             .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Slider))
              .Map(dest => dest.CategoryNameAr, src =>src.Category!=null? src.Category.NameAr:"")
               .Map(dest => dest.CategoryNameEn, src => src.Category != null ? src.Category.NameEn:"");
        }
    }
}
