using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Categories.Queries.GetParentCategories;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.MappingConfig;

public class CategoryResponseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryResponse>()
            .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Category));
    }
}