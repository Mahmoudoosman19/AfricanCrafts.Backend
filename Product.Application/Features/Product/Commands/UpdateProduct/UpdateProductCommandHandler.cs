using Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, string>
    {
        private readonly IProductUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product dbProduct = _unitOfWork.Repository<Domain.Entities.Product>()
                .GetEntityWithSpec(new GetProductDetailsByIdWithRelationsProductSpecification(new GetProductDetailsByIdWithRelationsProductQuery { Id = request.Id }))!;

            var updateProductDetailsHandler = new UpdateProductDetailsHandler(_unitOfWork, _mapper);
            var updateProductImagesHandler = new UpdateProductImagesHandler(_unitOfWork, _mapper);
            var updateProductExtensionsHandler = new UpdateProductExtensionsHandler(_unitOfWork, _mapper);
            updateProductDetailsHandler.SetNextHandler(updateProductImagesHandler).SetNextHandler(updateProductExtensionsHandler);
            await updateProductDetailsHandler.Handle(new UpdateProductInput(request, dbProduct, cancellationToken));
            dbProduct.UpdateEvent();

            await _unitOfWork.CompleteAsync(cancellationToken);

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}