using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using ImageKitFileManager.Helpers;
using Mapster;
using Product.Application.Features.Categories.Queries.GetParentCategories;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Features.Product.Queries.GetProductDetailsForCheckOut;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.MappingConfig;

public class ProductDetailsQueryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Domain.Entities.Product, ProductDetailsQueryResponse>()
            .Map(dest => dest.Images,
                 src => src.Images.Select(i => new ProductImageResponse()
                 {
                     Id = i.Id,
                     ImageUrl = GetImageUrl(src.ImagesFolderName, i.ImageName),
                     ColorCode = i.ColorCode
                 })
                 .ToList());
        config.NewConfig<Domain.Entities.ProductImage,ProductImageResponse >()
            .Map(dest => dest.ImageUrl, src => ImageKitBaseUrl.GenerateImageUrl(src.ImageName, FileType.Product));

        config.NewConfig<Domain.Entities.Product, GetProductDetailsForCheckOutQueryResponse>()
            .Map(dest => dest.Images,
                 src => src.Images.Select(i => new ProductImageResponse()
                 {
                     Id = i.Id,
                     ImageUrl = GetImageUrl(src.ImagesFolderName, i.ImageName),
                     ColorCode = i.ColorCode
                 })
                 .ToList());
    }


    private string GetImageUrl(Guid folderName, string imageName)
    {
        IImageKitService _imageKitService = MapContext.Current!.GetService<IImageKitService>();

        return $"{_imageKitService.GetBaseUrl()}/{FileType.Product.ToString().ToLower()}s/{folderName}/{imageName}";
    }
}