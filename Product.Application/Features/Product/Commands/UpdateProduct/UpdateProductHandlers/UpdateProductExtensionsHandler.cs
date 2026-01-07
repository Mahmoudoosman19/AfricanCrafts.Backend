using Common.Application.Extensions.Mapster;
using Product.Application.Abstractions;
using Product.Application.Features.Product.Commands.UpdateProduct.DTOs;
using Product.Application.Specifications.Products;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers
{
    internal class UpdateProductExtensionsHandler : ResponsibilityHandler<UpdateProductInput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductExtensionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task Handle(UpdateProductInput input)
        {
            List<ProductExtension> dbExtensions = _unitOfWork.Repository<ProductExtension>()
                .GetWithSpec(new GetProductExtensionsByProductIdSpecification(input.DBProduct.Id)).data.ToList();

            Dictionary<bool, List<UpdateProductExtensionDTO>> requestExtension = input.Request.Extensions
                .GroupBy(exe => exe.Id == Guid.Empty)
                .ToDictionary(group => group.Key, group => group.ToList());

            if (requestExtension.ContainsKey(false))
                UpdateAndDelete(input.DBProduct, dbExtensions, requestExtension[false]);
            else
                _unitOfWork.Repository<ProductExtension>().DeleteRange(dbExtensions);

            if (requestExtension.ContainsKey(true))
                Add(requestExtension[true], input.DBProduct, input.CancellationToken);

            await CallNext(input);
        }

        private void Add(List<UpdateProductExtensionDTO> extensionDTOs, Domain.Entities.Product product, CancellationToken cancellationToken)
        {
            List<ProductExtension> extensions = _mapper.From(extensionDTOs)
                .AdaptToType<List<ProductExtension>>();

            product.AddRangeExtension(extensions);
        }

        private void UpdateAndDelete(Domain.Entities.Product product, List<ProductExtension> dbExtensions, List<UpdateProductExtensionDTO> extensionDTOs)
        {
            List<ProductExtension> updatedExtensions = product.Extensions.Where(e => extensionDTOs.Any(d => d.Id == e.Id)).ToList();
            product.UpdateExtensions(updatedExtensions);
            extensionDTOs.UpdateNestedListObject<UpdateProductExtensionDTO, ProductExtension, Guid>(updatedExtensions, _mapper);

            List<ProductExtension> deletedExtensions = dbExtensions.Except(updatedExtensions).ToList();
            _unitOfWork.Repository<ProductExtension>().DeleteRange(deletedExtensions);
        }
    }
}
