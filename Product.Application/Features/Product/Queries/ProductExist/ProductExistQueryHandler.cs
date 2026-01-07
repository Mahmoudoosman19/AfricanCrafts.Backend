using Product.Application.Features.Product.Queries.IsProductExist;

namespace Product.Application.Features.Product.Queries.ProductExist
{
    internal class ProductExistQueryHandler : IQueryHandler<ProductExistQuery, bool>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;

        public ProductExistQueryHandler(IGenericRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ResponseModel<bool>> Handle(ProductExistQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepo.IsExistAsync(x => x.Id == request.Id, cancellationToken);

            return ResponseModel.Success(result);
        }
    }
}
