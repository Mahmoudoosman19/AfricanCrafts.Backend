using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Queries.ProductStatisticsQuery
{
    public class ProductStatisticsQueryHandler : IQueryHandler<ProductStatisticsQuery, ProductStatisticsQueryResponse>
    {
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;

        public ProductStatisticsQueryHandler(IProductRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }

       
    public async Task<ResponseModel<ProductStatisticsQueryResponse>> Handle(ProductStatisticsQuery request, CancellationToken cancellationToken)
    {
        // مواصفة لجلب المنتجات الأعلى تقييمًا
        var topRatedProductsSpec = new GetTopProductsByRateWithImageSpecification(5);
        var (topProducts,totalCount)=  _productRepo.GetWithSpec(topRatedProductsSpec);


        var response = new ProductStatisticsQueryResponse
        {
            TotalProductsCount = topProducts.Count(),
            TopProducts = topProducts.Select(p => new TopProductDto
            {
                ProductId = p.Id,
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                ProductCode = p.ProductCode,
                Rate = p.Rate
            }).ToList()
        };

        return ResponseModel.Success(response,totalCount);
    }
}
}
