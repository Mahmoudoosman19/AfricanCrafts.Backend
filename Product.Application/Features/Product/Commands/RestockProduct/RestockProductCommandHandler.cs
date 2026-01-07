using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.RestockProduct
{
    public class RestockProductCommandHandler : ICommandHandler<RestockProductCommand>
    {
        private readonly IGenericRepository<ProductExtension> _productextensionRepo;
        public RestockProductCommandHandler(IGenericRepository<ProductExtension> productextensionRepo)
        {
            _productextensionRepo = productextensionRepo;
        }

        public async Task<ResponseModel> Handle(RestockProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productextensionRepo.GetByIdAsync(request.ProductExtensionId);
            if (request.Increase)

                product.SetAmount(product.Amount + request.Amount);
            else
            product.SetAmount(product.Amount - request.Amount);
            await _productextensionRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);

        }
    }
}
