

using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    internal class GetProductDetailsByIdWithRelationsProductQueryHandler : IQueryHandler<GetProductDetailsByIdWithRelationsProductQuery, ProductDetailsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;

        public GetProductDetailsByIdWithRelationsProductQueryHandler(IMapper mapper,
            IProductRepository<Domain.Entities.Product> productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public Task<ResponseModel<ProductDetailsQueryResponse>> Handle(GetProductDetailsByIdWithRelationsProductQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepo.GetEntityWithSpec(new GetProductDetailsByIdWithRelationsProductSpecification(request));

            var response = _mapper.Map<ProductDetailsQueryResponse>(product!);
            

            return Task.FromResult(ResponseModel.Success(response, 1));
        }
    }
}
