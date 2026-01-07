using IdentityHelper.Abstraction;

namespace Product.Application.Features.Review.Commands.AddReview
{
    internal class AddReviewCommandHandler : ICommandHandler<AddReviewCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;
       
        public AddReviewCommandHandler(
            IGenericRepository<Domain.Entities.Review> reviewRepo,
            IGenericRepository<Domain.Entities.Product> productRepo,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Domain.Entities.Review>(request);

            var product = await _productRepo.GetByIdAsync(request.ProductId);

            if (product.IsActive == true)
            {
                var userId = _tokenExtractor.GetUserId();

                review.SetUserId(userId);

                await _reviewRepo.AddAsync(review, cancellationToken);

                await _reviewRepo.SaveChangesAsync(cancellationToken);

                review.RaiseRateUpdatesDomainEvents(product!.VendorId);

                return ResponseModel.Success();

            }
            return ResponseModel.Failure(Messages.ThisProductIsNotAvailable);
        }
    }
}
