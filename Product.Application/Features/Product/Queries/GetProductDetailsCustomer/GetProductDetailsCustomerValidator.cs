using Product.Domain.Enums;

namespace Product.Application.Features.Product.Queries.GetProductDetailsCustomer
{
    internal class GetProductDetailsCustomerValidator : AbstractValidator<CustomerGetProductDetailsByIdQuery>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        public GetProductDetailsCustomerValidator(IGenericRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
            RuleFor(x => x.Id)
                .EntityExist(productRepo).WithMessage(Messages.NotFound)
                .MustAsync(CheckProduct).WithMessage(Messages.NotFound);
        }

        private async Task<bool> CheckProduct(Guid id, CancellationToken cancellationToken)
        {
            var result = await _productRepo.GetByIdAsync(id);
            if (result != null && result.Status != ProductStatus.Approved || result.IsActive!=true)
            {
                return false;
            }
            return true;
         
        }
    }
}
