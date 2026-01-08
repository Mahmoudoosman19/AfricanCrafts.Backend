using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.ChangeProductActivation
{
    internal class ToggleProductActivationCommandHandler : ICommandHandler<ToggleProductActivationCommand>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;

        public ToggleProductActivationCommandHandler(IProductRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ResponseModel> Handle(ToggleProductActivationCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.ProductId, cancellationToken);

            product!.SetActivation(!product.IsActive);

            await _productRepo.SaveChangesAsync(cancellationToken);

            return ResponseModel.Success();
        }
    }
}
