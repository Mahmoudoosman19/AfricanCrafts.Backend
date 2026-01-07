using IdentityHelper.Abstraction;
using Product.Application.Specifications.Reviews;

namespace Product.Application.Features.Review.Commands.UpdateReview
{
    internal class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly ITokenExtractor _tokenExtractor;

        public UpdateReviewCommandHandler(IGenericRepository<Domain.Entities.Review> reviewRepo, IGenericRepository<Domain.Entities.Product> productRepo, ITokenExtractor tokenExtractor)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.ProductId);

            if (product == null || product.IsActive == false)
                return ResponseModel.Failure(Messages.ThisProductIsNotAvailable);

            var userId = _tokenExtractor.GetUserId();
            var productReviewSpec = new GetReviewByUserIdAndProductReviewIdSpecification(request.Id, userId);
            var productReview = _reviewRepo.GetEntityWithSpec(productReviewSpec);

            productReview!.SetRate(request.Rate);
            productReview.SetComment(request.Comment);

            _reviewRepo.Update(productReview);
            await _reviewRepo.SaveChangesAsync(cancellationToken);

            productReview.RaiseRateUpdatesDomainEvents(product!.VendorId);
            return ResponseModel.Success();
        }
    }
}