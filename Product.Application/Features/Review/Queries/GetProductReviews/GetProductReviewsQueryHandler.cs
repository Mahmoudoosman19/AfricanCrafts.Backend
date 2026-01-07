using IdentityHelper.Abstraction;
using Product.Application.Specifications.Reviews;
using review = Product.Domain.Entities.Review;
namespace Product.Application.Features.Review.Queries.GetProductReviews
{
    internal class GetProductReviewsQueryHandler : IQueryHandler<GetProductReviewsQuery, List<GetProductReviewsQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<review> _reviewRepo;
     //   private readonly ITokenExtractor _tokenExtractor;
        private readonly IUserManagement _userService;

        public GetProductReviewsQueryHandler(IMapper mapper, IGenericRepository<review> reviewRepo, IUserManagement userService)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
           // _tokenExtractor = tokenExtractor;
            _userService = userService;
        }
        public async Task<ResponseModel<List<GetProductReviewsQueryResponse>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PageIndex - 1) * request.PageSize;
            
            (var reviews, int count) =
                _reviewRepo.GetWithSpec(new GetReviewsByProductIdAndVendorIdSpecification(request.ProductId , request));

            var mappingReview = _mapper.Map<List<GetProductReviewsQueryResponse>>(reviews);

            var tasks = mappingReview.Select(async item =>
            {
                var userDate = await _userService.GetUserData(item.UserId);
                item.UserName = userDate!.UserName;
                return item;
            }).ToList();

            var items = await Task.WhenAll(tasks);

            return ResponseModel.Success(items.ToList(), count);
        }
    }
}
