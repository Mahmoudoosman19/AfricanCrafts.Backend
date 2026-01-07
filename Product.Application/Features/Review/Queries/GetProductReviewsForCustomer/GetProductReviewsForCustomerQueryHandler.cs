using Product.Application.Specifications.Reviews;
using review = Product.Domain.Entities.Review;
namespace Product.Application.Features.Review.Queries.GetProductReviewsForCustomer
{
    internal class GetProductReviewsForCustomerQueryHandler : IQueryHandler<GetProductReviewsForCustomerQuery, GetProductReviewsForCustomerQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<review> _reviewRepo;

        public GetProductReviewsForCustomerQueryHandler(IMapper mapper, IGenericRepository<review> reviewRepo)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
        }
        public async Task<ResponseModel<GetProductReviewsForCustomerQueryResponse>> Handle(GetProductReviewsForCustomerQuery request, CancellationToken cancellationToken)
        {
            var reviews = _reviewRepo.GetEntityWithSpec(new GetReviewByUserIdAndProductIdSpecification(request.ProductId, request.CustomerId));
            if (reviews == null)
                return ResponseModel.Failure<GetProductReviewsForCustomerQueryResponse>(Messages.NotFound);

            var mappingReview = _mapper.Map<GetProductReviewsForCustomerQueryResponse>(reviews);

            return ResponseModel.Success(mappingReview);
        }
    }
}
