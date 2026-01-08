using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Queries.GetProductDetailsForCheckOut
{
    class GetProductDetailsForCheckOutQueryHandler : IQueryHandler<GetProductDetailsForCheckOutQuery, GetProductDetailsForCheckOutQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;
        private readonly IProductRepository<Domain.Entities.ProductExtension> _productExtensionRepo;

        public GetProductDetailsForCheckOutQueryHandler(IMapper mapper,
            IProductRepository<Domain.Entities.Product> productRepo,
            IProductRepository<Domain.Entities.ProductExtension> productExtensionRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _productExtensionRepo = productExtensionRepo;
        }

        public async Task<ResponseModel<GetProductDetailsForCheckOutQueryResponse>> Handle(GetProductDetailsForCheckOutQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepo.GetEntityWithSpec(new GetProductDetailsByIdForCheckOutSpecification(request));

            if (product == null)
                return ResponseModel.Failure<GetProductDetailsForCheckOutQueryResponse>(Messages.NotFound);

            var productExtensions = product.Extensions.Where(x => x.Id == request.extensionsId).FirstOrDefault();

            if (productExtensions == null || productExtensions.Amount == 0)
                return ResponseModel.Failure<GetProductDetailsForCheckOutQueryResponse>(Messages.ProductExtensionNotFound);

            productExtensions.SetAmount(productExtensions.Amount - request.Quantity);

            _productExtensionRepo.Update(productExtensions);
            await _productExtensionRepo.SaveChangesAsync();

            var response = _mapper.Map<GetProductDetailsForCheckOutQueryResponse>(product!);

            return ResponseModel.Success(response, 1);
        }
    }
}


