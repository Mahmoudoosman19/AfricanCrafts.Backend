

using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetProductDetails
{
    internal class GetProductDetailsByIdWithRelationsProductQueryHandler : IQueryHandler<GetProductDetailsByIdWithRelationsProductQuery, ProductDetailsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;

        public GetProductDetailsByIdWithRelationsProductQueryHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Product> productRepo)
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
