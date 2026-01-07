

using Product.Application.Specifications.Products;
using Product.Application.Specifications.Reviews;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    internal class GetProductDetailsByIdWithRelationsProductQueryHandler : IQueryHandler<GetProductDetailsByIdWithRelationsProductQuery, ProductDetailsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;

        public GetProductDetailsByIdWithRelationsProductQueryHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Product> productRepo, IGenericRepository<Domain.Entities.Review> reviewRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
        }

        public Task<ResponseModel<ProductDetailsQueryResponse>> Handle(GetProductDetailsByIdWithRelationsProductQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepo.GetEntityWithSpec(new GetProductDetailsByIdWithRelationsProductSpecification(request));

            var response = _mapper.Map<ProductDetailsQueryResponse>(product!);
            var reviewCount = _reviewRepo.GetWithSpec(new GetReviewByProductIdSpecification(request.Id)).count;
            response.NumberOfRatings = reviewCount;

            return Task.FromResult(ResponseModel.Success(response, 1));
        }
    }
}
