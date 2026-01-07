using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using Mapster;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Features.Product.Queries.GetProductDetailsForCheckOut;

namespace Product.Application.Features.Product.MappingConfig;

public class ProductDetailsQueryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Domain.Entities.Product, ProductDetailsQueryResponse>()
            .Map(dest => dest.Images,
                 src => src.Images.Select(i => new DetailedProductImageResponse()
                 {
                     Id = i.Id,
                     ImageUrl = GetImageUrl(src.ImagesFolderName, i.ImageName),
                     ColorCode = i.ColorCode
                 })
                 .ToList());

        config.NewConfig<Domain.Entities.Product, GetProductDetailsForCheckOutQueryResponse>()
            .Map(dest => dest.Images,
                 src => src.Images.Select(i => new DetailedProductImageResponse()
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