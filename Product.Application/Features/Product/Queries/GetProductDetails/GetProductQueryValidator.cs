namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    internal class GetProductQueryValidator : AbstractValidator<GetProductDetailsByIdWithRelationsProductQuery>
    {
        public GetProductQueryValidator(IGenericRepository<Domain.Entities.Product> productRepo)
        {
            RuleFor(x => x.Id)
                .EntityExist(productRepo).WithMessage(Messages.NotFound);
        }
    }
}
