using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.SupervisorProductAdmission
{
    public class SupervisorProductAdmissionValidater:AbstractValidator<SupervisorProductAdmissionCommand>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;
        public SupervisorProductAdmissionValidater(
            IProductRepository<Domain.Entities.Product> productRepo
           )
        {
            _productRepo = productRepo;

            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .EntityExist(productRepo).WithMessage(Messages.NotFound)
                .MustAsync(VerifyProduct)
                .WithMessage(Messages.IncorrectData);
        }
        private async Task<bool> VerifyProduct(Guid productId, CancellationToken cancellationToken)
        {

            return await _productRepo.IsExistAsync
                (p => p.Id == productId, cancellationToken);
        }

    }
}
