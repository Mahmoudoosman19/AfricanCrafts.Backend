using Common.Application.Extensions.Mapster;
using Product.Application.Abstractions;
using Product.Application.Features.Product.Commands.AddProduct.DTOs;
using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using Product.Application.Features.Product.MappingConfig;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers
{
    internal class UpdateProductImagesHandler : ResponsibilityHandler<UpdateProductInput>
    {
        private readonly IProductUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductImagesHandler(IProductUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task Handle(UpdateProductInput input)
        {
            List<ProductImage> dbImages = _unitOfWork.Repository<ProductImage>()
               .GetWithSpec(new GetProductImagesByProductIdSpecification(input.DBProduct.Id)).data.ToList();

            Dictionary<bool, List<UpdateProductImageDTO>> requestImages = input.Request.Images
                .GroupBy(img => img.Id == Guid.Empty)
                .ToDictionary(group => group.Key, group => group.ToList());

            if (requestImages.ContainsKey(false))
                UpdateAndDelete(input.DBProduct, dbImages, requestImages[false]);
            else
                _unitOfWork.Repository<ProductImage>().DeleteRange(dbImages);

            if (requestImages.ContainsKey(true))
                Add(requestImages[true], input.DBProduct, input.CancellationToken);

            await CallNext(input);
        }

        private void Add(List<UpdateProductImageDTO> imageDTOs, Domain.Entities.Product product, CancellationToken cancellationToken)
        {
            List<AddProductImageDTO> addImageDTOs = imageDTOs.Select(dto => new AddProductImageDTO
            {
                ColorCode = dto.ColorCode,
                ImageFile = dto.ImageFile
            }).ToList();
            List<ProductImage> images = _mapper.From(addImageDTOs)
                .AddParameters(ProductMappingConfigParametersKeys.FolderId, product.ImagesFolderName)
                .AdaptToType<List<ProductImage>>();

            product.AddRangeImage(images);
        }

        private void UpdateAndDelete(Domain.Entities.Product product, List<ProductImage> dbImages, List<UpdateProductImageDTO> imageDTOs)
        {
            List<ProductImage> updatedImages = product.Images.Where(i => imageDTOs.Any(d => d.Id == i.Id)).ToList();
            foreach (var image in imageDTOs)
            {
                updatedImages.First(img => img.Id.Equals(image.Id)).SetColor(image.ColorCode);
            }
            product.UpdateImages(updatedImages);
            imageDTOs.UpdateNestedListObject<UpdateProductImageDTO, ProductImage, Guid>(updatedImages, _mapper);

            List<ProductImage> deletedImages = dbImages.Except(updatedImages).ToList();
            _unitOfWork.Repository<ProductImage>().DeleteRange(deletedImages);
        }
    }
}
