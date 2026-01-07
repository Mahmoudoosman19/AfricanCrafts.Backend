using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using Mapster;
using Product.Application.Features.Favorite.Qeury.ListFavorite;

namespace Product.Application.Features.Favorite.MappingConfig
{
    internal class FavoriteMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Favorite, ListFavoriteQueryResponse>()
               .Map(dest => dest.DiscountPrice, src => src.Product.DiscountPrice)
               .Map(x => x.productName, z => z.Product.NameAr)
                .Map(x => x.Price, z => z.Product.Price)
                 .Map(x => x.ProductId, z => z.Product.Id)
                  .Map(dest => dest.Images, src =>  GetImageUrl(src.Product));
        


        }
        private string GetImageUrl(Domain.Entities.Product product)
        {
            var image =product.Images.FirstOrDefault();
            if(image != null)
            {
                IImageKitService _imageKitService = MapContext.Current!.GetService<IImageKitService>();

                return $"{_imageKitService.GetBaseUrl()}/{FileType.Product.ToString().ToLower()}s/{product.ImagesFolderName}/{image.ImageName}";
            }
            return "";
        }

    }
}
