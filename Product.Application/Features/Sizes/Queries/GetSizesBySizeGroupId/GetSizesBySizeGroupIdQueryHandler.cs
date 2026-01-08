using Product.Application.Specifications.Sizes;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.Queries.GetSizesBySizeGroupId
{
    internal class GetSizesBySizeGroupIdQueryHandler : IQueryHandler<GetSizesBySizeGroupIdQuery, IReadOnlyList<GetSizesResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Size> _sizeRepo;

        public GetSizesBySizeGroupIdQueryHandler(
            IMapper mapper,
            IProductRepository<Size> sizeRepo)
        {
            _mapper = mapper;
            _sizeRepo = sizeRepo;
        }
        public Task<ResponseModel<IReadOnlyList<GetSizesResponse>>> Handle(GetSizesBySizeGroupIdQuery request, CancellationToken cancellationToken)
        {
            (var sizes, int count) = _sizeRepo.GetWithSpec(new GetSizesBySizeGroupIdAndStatusSpecifications(request.Id));
            var mappingSizes = _mapper.Map<IReadOnlyList<GetSizesResponse>>(sizes);
            return Task.FromResult(ResponseModel.Success(mappingSizes, count));
        }


    }
}
