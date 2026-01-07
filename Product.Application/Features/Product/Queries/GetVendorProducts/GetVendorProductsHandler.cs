using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetVendorProduct
{
    public class GetVendorProductsHandler : IQueryHandler<VendorGetProductsByVendorIdQuery, GetVendorProductsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        public GetVendorProductsHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Product> productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }
        public  Task<ResponseModel<GetVendorProductsResponse>> Handle(VendorGetProductsByVendorIdQuery request, CancellationToken cancellationToken)
        {
         var product  = _productRepo.GetWithSpec(new VendorGetProductByVendorIdSpecification(request)).data.ToList();
            // var products = _productRepo.Get();
            var response = new GetVendorProductsResponse()
            {
                ProductIds = product.Select(x=>x.Id).ToList(),
            };

            return Task.FromResult(ResponseModel.Success(response));
        }
    }
}
