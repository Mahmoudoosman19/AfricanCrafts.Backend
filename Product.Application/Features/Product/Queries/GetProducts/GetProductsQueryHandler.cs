using IdentityHelper.Abstraction;
using Mapster;
using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetProducts
{
    internal class GetProductsQueryHandler : IQueryHandler<GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery, IReadOnlyList<ProductQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;

        public GetProductsQueryHandler(
            IGenericRepository<Domain.Entities.Product> productRepo,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<IReadOnlyList<ProductQueryResponse>>> Handle(GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var role = _tokenExtractor.GetUserRole();
            
            (var products, int count) = _productRepo.GetWithSpec(new GetProductsByStatusAndProductCodeAndNameWithImageSpecification(request, userId, role));
            var reviewCounts = products.ToDictionary(
                product => product.Id
            );

            var response = _mapper.Map<IReadOnlyList<ProductQueryResponse>>(products).ToList();


            return ResponseModel.Success<IReadOnlyList<ProductQueryResponse>>(response, count);
        }
    }
}
