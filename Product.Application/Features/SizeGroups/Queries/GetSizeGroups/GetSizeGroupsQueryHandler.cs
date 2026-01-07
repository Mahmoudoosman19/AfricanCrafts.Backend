using Product.Application.SharedDTOs.SizeGroup;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroups
{
    internal class GetSizeGroupsQueryHandler :
        IQueryHandler<GetSizeGroupsQuery, IEnumerable<SizeGroupLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;

        public GetSizeGroupsQueryHandler(
            IMapper mapper,
            IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _mapper = mapper;
            _sizeGroupRepo = sizeGroupRepo;
        }

        public Task<ResponseModel<IEnumerable<SizeGroupLookupDto>>> Handle(GetSizeGroupsQuery request, CancellationToken cancellationToken)
        {
            var (sizeGroupsQuery, count) = _sizeGroupRepo
                .GetWithSpec(new GetSizeGroupsSpecification(request));

            var response = _mapper.Map<IEnumerable<SizeGroupLookupDto>>(sizeGroupsQuery);
            return Task.FromResult(ResponseModel.Success(response, count));
        }
    }
}