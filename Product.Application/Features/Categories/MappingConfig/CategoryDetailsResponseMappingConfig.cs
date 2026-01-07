using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Categories.Queries.GetCategroyById;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.MappingConfig
{
    internal class CategoryDetailsResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, GetCategoryDetailsResponse>()
              .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Category));
        }

    }
}
