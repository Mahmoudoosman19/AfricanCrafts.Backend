using IdentityHelper.Abstraction;
using Mapster;
using Product.Application.Specifications.Products;
using Product.Application.Specifications.Reviews;

namespace Product.Application.Features.Product.Queries.GetProducts
{
    internal class GetProductsQueryHandler : IQueryHandler<GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery, IReadOnlyList<ProductQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;

        public GetProductsQueryHandler(
            IGenericRepository<Domain.Entities.Product> productRepo,
            IGenericRepository<Domain.Entities.Review> reviewRepo,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<IReadOnlyList<ProductQueryResponse>>> Handle(GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var role = _tokenExtractor.GetUserRole();
            
            (var products, int count) = _productRepo.GetWithSpec(new GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageSpecification(request, userId, role));
            var reviewCounts = products.ToDictionary(
                product => product.Id,
                product => _reviewRepo.GetWithSpec(new GetReviewByProductIdSpecification(product.Id)).count
            );

            var response = _mapper.Map<IReadOnlyList<ProductQueryResponse>>(products)
           .Select(item =>
           {
               item.NumberOfRatings = reviewCounts.ContainsKey(item.Id) ? reviewCounts[item.Id] : 0;
               return item;
           })
           .ToList();


            return ResponseModel.Success<IReadOnlyList<ProductQueryResponse>>(response, count);
        }
    }
}
