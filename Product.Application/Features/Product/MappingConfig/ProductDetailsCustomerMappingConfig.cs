using Common.Application.Localization;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using Mapster;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Features.Product.Queries.GetProductDetailsCustomer;

namespace Product.Application.Features.Product.MappingConfig
{
    internal class ProductDetailsCustomerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<Domain.Entities.Product, CustomerProductDetailsQueryResponse>()
                .Map(dest=>dest.Description,src=> GetDescription(src))
                 .Map(dest => dest.Name, src => GetName(src))
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
        private string GetDescription(Domain.Entities. Product product) { 
            ILocalizer localizer = MapContext.Current!.GetService<ILocalizer>();
            var lang=localizer.GetLanguage();   
            if (lang == "ar") {
                return product.DescriptionAr;
            }
            return product.DescriptionEn;   
        }
        private string GetName(Domain.Entities.Product product)
        {
            ILocalizer localizer = MapContext.Current!.GetService<ILocalizer>();
            var lang = localizer.GetLanguage();
            if (lang == "ar")
            {
                return product.NameAr;
            }
            return product.NameEn;
        }
    }
}

