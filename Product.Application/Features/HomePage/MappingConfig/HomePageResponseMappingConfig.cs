using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.HomePage.Query.Categories;
using Product.Application.Features.HomePage.Query.Sliders;
using Product.Domain.Entities;

namespace Product.Application.Features.HomePage.MappingConfig
{
    internal class HomePageResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Slider
            config.NewConfig<Slider, GetSliderQueryResponse>()
             .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Slider))
              .Map(dest => dest.CategoryNameAr, src => src.Category != null ? src.Category.NameAr : "")
               .Map(dest => dest.CategoryNameEn, src => src.Category != null ? src.Category.NameEn : "");

            //Category
            config.NewConfig<Category, GetCategoriesQueryResponse>()
          .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Category));
           
        }
    }
}
