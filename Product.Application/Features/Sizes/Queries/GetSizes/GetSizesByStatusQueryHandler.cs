using Product.Application.Specifications.Sizes;
using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.Queries.GetSizes
{
    internal class GetSizesByStatusQueryHandler : IQueryHandler<GetSizesByStatusQuery, List<GetSizesQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Size> _sizeRepo;
        public GetSizesByStatusQueryHandler(IMapper mapper, IGenericRepository<Size> sizeRepo)
        {
            _mapper = mapper;
            _sizeRepo = sizeRepo;
        }
        public async Task<ResponseModel<List<GetSizesQueryResponse>>> Handle(GetSizesByStatusQuery request, CancellationToken cancellationToken)
        {
            (var sizes, int count) = _sizeRepo.GetWithSpec(new GetSizesBySizeGroupIdAndStatusSpecifications(request));
            var mappingSizes = _mapper.Map<List<GetSizesQueryResponse>>(sizes);
            return ResponseModel.Success(mappingSizes, count);
        }
    }
}
