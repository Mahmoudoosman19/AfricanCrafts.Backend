namespace Product.Application.Features.Review.Queries.GetProductReviewsForCustomer
{
    internal class GetProductReviewsForCustomerQueryValidator : AbstractValidator<GetProductReviewsForCustomerQuery>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;

        public GetProductReviewsForCustomerQueryValidator(IGenericRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage(Messages.EmptyField);

            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .EntityExist(productRepo).WithMessage(Messages.NotFound);
        }
    }
}
