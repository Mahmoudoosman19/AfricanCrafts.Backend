using IdentityHelper.Abstraction;

namespace Product.Application.Features.Review.Queries.GetProductReviews
{
    internal class GetProductReviewsQueryValidator : AbstractValidator<GetProductReviewsQuery>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
     //   private readonly ITokenExtractor _tokenExtractor;

        public GetProductReviewsQueryValidator(
            IGenericRepository<Domain.Entities.Product> productRepo
            //,ITokenExtractor tokenExtractor
            )
        {
            _productRepo = productRepo;
            //  _tokenExtractor = tokenExtractor;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(productRepo).WithMessage(Messages.NotFound);
               // .MustAsync(BeOwnedByVendorIfVendor).WithMessage(Messages.VendorGetProductsReviewDoesNotOwn);
        }

        private async Task<bool> BeOwnedByVendorIfVendor(Guid productId, CancellationToken cancellationToken)
        {
            //var userId = _tokenExtractor.GetUserId();
            //var userRole = _tokenExtractor.GetUserRole();

            //if (userRole == "Vendor")
            //{
            //    if (product == null || product.VendorId != userId)
            //    {
            //        return false;
            //    }
            //}
           
            //if (userRole == "Vendor")
            //{
            //    var product = await _productRepo.GetByIdAsync(productId);
            //    if (product == null || product.VendorId != userId)
            //    {
            //        return false;
            //    }
            //}
            
            var product = await _productRepo.GetByIdAsync(productId, cancellationToken);
            return true;
        }
    }
}
