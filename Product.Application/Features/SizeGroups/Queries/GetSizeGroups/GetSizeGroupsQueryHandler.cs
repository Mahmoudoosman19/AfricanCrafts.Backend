using Product.Application.SharedDTOs.SizeGroup;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroups
{
    internal class GetSizeGroupsQueryHandler :
        IQueryHandler<GetSizeGroupsQuery, IEnumerable<SizeGroupLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<SizeGroup> _sizeGroupRepo;

        public GetSizeGroupsQueryHandler(
            IMapper mapper,
            IProductRepository<SizeGroup> sizeGroupRepo)
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