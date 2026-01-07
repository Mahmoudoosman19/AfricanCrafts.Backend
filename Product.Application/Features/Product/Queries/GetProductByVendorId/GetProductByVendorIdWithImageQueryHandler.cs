using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetProductByVendorId
{
    public class GetProductByVendorIdWithImageQueryHandler : IQueryHandler<GetProductByVendorIdWithImageQuery, List<GetProductQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly IGenericRepository<Domain.Entities.ProductImage> _productImageRepo;
        private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;
        private readonly IMapper _mapper;

        public GetProductByVendorIdWithImageQueryHandler(
            IGenericRepository<Domain.Entities.Product> productRepo,
            IMapper mapper,
            IGenericRepository<Domain.Entities.ProductImage> productImageRepo,
            IGenericRepository<Domain.Entities.Review> reviewRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _productImageRepo = productImageRepo;
            _reviewRepo = reviewRepo;
        }

        public async Task<ResponseModel<List<GetProductQueryResponse>>> Handle(GetProductByVendorIdWithImageQuery query, CancellationToken cancellationToken)
        {
            var products = _productRepo.GetWithSpec(
                new GetProductByVendorIdWithImageSpecification(query.VendorId)).data.ToList();

            if (!products.Any())
            {
                return ResponseModel.Failure<List<GetProductQueryResponse>>(Messages.Noproductsfoundforthegivenvendor);
            }

            var productIds = products.Select(p => p.Id).ToList();

            var reviews = _reviewRepo.Get().ToList();

            var productResponse = _mapper.Map<List<GetProductQueryResponse>>(products);
            var productResponses = productResponse.Select(product =>
            {
                var productReviews = reviews.Where(review => review.ProductId == product.ProductId).ToList();
                product.Review = productReviews;
                return product;
            }).ToList();

            return ResponseModel.Success(productResponses);
        }
    }
}
