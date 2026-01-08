using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using Mapster;
using Product.Application.Features.Product.Commands.AddProduct;
using Product.Application.Features.Product.Commands.AddProduct.DTOs;
using Product.Application.Features.Product.Commands.SuperVisorAddProduct;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.MappingConfig
{
    internal class AddProductCommandMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddProductCommand, Domain.Entities.Product>()
                .AfterMapping(MapLists);

            config.NewConfig<SuperVisorAddProductCommand, Domain.Entities.Product>()
                .AfterMapping(MapListsSupervisor);

            config.NewConfig<AddProductImageDTO, ProductImage>()
                .AfterMapping(MapFile);

            config.NewConfig<AddProductExtensionDTO, ProductExtension>();
        }

        private void MapLists(AddProductCommand dest, Domain.Entities.Product src)
        {
            src.SetImagesFolderName(Guid.NewGuid());
            MapContext.Current!.Parameters[ProductMappingConfigParametersKeys.FolderId] = src.ImagesFolderName;

            var extensions = dest.Extensions.Adapt<List<ProductExtension>>();
            var images = dest.Images.Adapt<List<ProductImage>>();

            src.AddRangeExtension(extensions);
            src.AddRangeImage(images);
        }

        private void MapListsSupervisor(SuperVisorAddProductCommand dest, Domain.Entities.Product src)
        {
            src.SetImagesFolderName(Guid.NewGuid());
            MapContext.Current!.Parameters[ProductMappingConfigParametersKeys.FolderId] = src.ImagesFolderName;

            var extensions = dest.Extensions.Adapt<List<ProductExtension>>();
            var images = dest.Images.Adapt<List<ProductImage>>();

            src.AddRangeExtension(extensions);
            src.AddRangeImage(images);
        }

        private Guid GetCurrentUserId()
        {
            var currentUser = MapContext.Current!.GetService<ITokenExtractor>();
            return currentUser.GetUserId();
        }

        private void MapFile(AddProductImageDTO dest, ProductImage src)
        {
            IImageKitService imageKitService = MapContext.Current!.GetService<IImageKitService>();
            Guid folderId = (Guid)MapContext.Current!.Parameters[ProductMappingConfigParametersKeys.FolderId];
            src.SetImage(imageKitService, dest.ImageFile, folderId);
        }
    }
}
