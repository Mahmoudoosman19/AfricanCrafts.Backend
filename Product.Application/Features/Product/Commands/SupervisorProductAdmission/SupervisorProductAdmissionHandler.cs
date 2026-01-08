using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.SupervisorProductAdmission
{
    internal class SupervisorProductAdmissionHandler : ICommandHandler<SupervisorProductAdmissionCommand>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;
        public SupervisorProductAdmissionHandler(IProductRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ResponseModel> Handle(SupervisorProductAdmissionCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.ProductId, cancellationToken);

            product!.SetStatus(Domain.Enums.ProductStatus.Approved);
            product!.SetActivation(true);

            await _productRepo.SaveChangesAsync(cancellationToken);

            return ResponseModel.Success();
        }
    }
}
