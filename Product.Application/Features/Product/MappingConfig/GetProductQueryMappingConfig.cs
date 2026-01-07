using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using Mapster;
using Product.Application.Features.Product.Queries.GetProducts;

namespace Product.Application.Features.Product.MappingConfig;

internal class GetProductQueryMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Domain.Entities.Product, ProductQueryResponse>()
            .Map(dest=>dest.ViewStatus ,src=>(src.Status))
            .Map(dest => dest.Images,
                 src => src.Images.Select(i => new ProductImageResponse
                 {
                     ImageName = GetImageUrl(src.ImagesFolderName, i.ImageName)
                 })
                 .ToList());
    }

    private string GetImageUrl(Guid folderName, string imageName)
    {
        IImageKitService _imageKitService = MapContext.Current.GetService<IImageKitService>();

        return $"{_imageKitService.GetBaseUrl()}/{FileType.Product.ToString().ToLower()}s/{folderName}/{imageName}";
    }
}
