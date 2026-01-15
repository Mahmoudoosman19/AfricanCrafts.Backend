using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Specifications.Products;
using Product.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Queries.GetProductsBulkWithRelations
{
    internal class GetProductsBulkWithRelationsQueryHandler
    : IQueryHandler<GetProductsBulkWithRelationsQuery, List<ProductDetailsQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Domain.Entities.Product> _productRepo;

        public GetProductsBulkWithRelationsQueryHandler(IMapper mapper,
            IProductRepository<Domain.Entities.Product> productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<ResponseModel<List<ProductDetailsQueryResponse>>> Handle(
     GetProductsBulkWithRelationsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductsBulkWithRelationsSpecification(request);

            var (query, totalCount) = _productRepo.GetWithSpec(spec);

            var products = await query.ToListAsync(cancellationToken);

            var response = _mapper.Map<List<ProductDetailsQueryResponse>>(products);

            return ResponseModel.Success(response, response.Count);
        }
    }
}
