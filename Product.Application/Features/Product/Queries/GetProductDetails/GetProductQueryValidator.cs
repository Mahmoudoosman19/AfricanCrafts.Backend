using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    internal class GetProductQueryValidator : AbstractValidator<GetProductDetailsByIdWithRelationsProductQuery>
    {
        public GetProductQueryValidator(IProductRepository<Domain.Entities.Product> productRepo)
        {
            RuleFor(x => x.Id)
                .EntityExist(productRepo).WithMessage(Messages.NotFound);
        }
    }
}
