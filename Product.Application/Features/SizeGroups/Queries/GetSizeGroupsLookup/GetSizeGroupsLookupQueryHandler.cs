using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupsLookup
{
    internal class GetSizeGroupsLookupQueryHandler : IQueryHandler<GetSizeGroupsLookupQuery, IReadOnlyList<GetSizeGroupsLookupResponse>>
    {
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;
        private readonly IMapper _mapper;

        public GetSizeGroupsLookupQueryHandler(
            IGenericRepository<SizeGroup> sizeGroupRepo, IMapper mapper)
        {
            _sizeGroupRepo = sizeGroupRepo;
            _mapper = mapper;

        }
        public Task<ResponseModel<IReadOnlyList<GetSizeGroupsLookupResponse>>> Handle(GetSizeGroupsLookupQuery request, CancellationToken cancellationToken)
        {
            var sizeGroups = _sizeGroupRepo.Get();
            var mappingSizeGroups = _mapper.Map<IReadOnlyList<GetSizeGroupsLookupResponse>>(sizeGroups);
            return Task.FromResult(ResponseModel.Success(mappingSizeGroups));

        }
    }
}
