using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Queries.GetProductDetailsCustomer
{
    internal class CustomerGetProductDetailsByIdQueryHandler:IQueryHandler<CustomerGetProductDetailsByIdQuery,CustomerProductDetailsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;

        public CustomerGetProductDetailsByIdQueryHandler(IMapper mapper,
            IProductRepository<Domain.Entities.Product> productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public Task<ResponseModel<CustomerProductDetailsQueryResponse>> Handle(CustomerGetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepo.GetEntityWithSpec(new CustomerGetDetailsProductDetailsByIdWithImageSpecification(request));

            var response = _mapper.Map<CustomerProductDetailsQueryResponse>(product!);

            return Task.FromResult(ResponseModel.Success(response, 1));
        }
    }
}
    

