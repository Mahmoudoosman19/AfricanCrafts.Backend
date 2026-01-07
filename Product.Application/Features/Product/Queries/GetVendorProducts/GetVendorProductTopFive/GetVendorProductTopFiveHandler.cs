using IdentityHelper.Abstraction;
using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetVendorProducts.GetVendorProductTopFive
{
    public class GetVendorProductTopFiveHandler : IQueryHandler<GetVendorProductTopFiveQuery, IEnumerable<GetVendorProductTopFiveResponce>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        private readonly ITokenExtractor _tokenExtractor;
        public GetVendorProductTopFiveHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Product> productRepo,
            ITokenExtractor tokenExtractor)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<IEnumerable<GetVendorProductTopFiveResponce>>> Handle(GetVendorProductTopFiveQuery request, CancellationToken cancellationToken)
        {
            request.VendorId = _tokenExtractor.GetUserId();
            var product = _productRepo.GetWithSpec(new VendorGetProductByVendorIdSpecification(request));
            if (!product.data.Any())
                return ResponseModel.Failure<IEnumerable<GetVendorProductTopFiveResponce>>(Messages.NotFound);

            var mapper = _mapper.Map<IEnumerable<GetVendorProductTopFiveResponce>>(product.data);

            return ResponseModel.Success(mapper, mapper.Count());
        }
    }
}
