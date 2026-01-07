using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    internal sealed class GetSizeGroupByIdQueryHandler
        : IQueryHandler<GetSizeGroupByIdQuery, SizeGroupWithSizesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;

        public GetSizeGroupByIdQueryHandler(
            IMapper mapper,
            IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _mapper = mapper;
            _sizeGroupRepo = sizeGroupRepo;
        }

        public async Task<ResponseModel<SizeGroupWithSizesResponse>> Handle(GetSizeGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var sizeGroup = _sizeGroupRepo.GetEntityWithSpec(new GetSizeGroupByIdSpecification(request));

            var response = _mapper.Map<SizeGroupWithSizesResponse>(sizeGroup!);

            return ResponseModel.Success(response);
        }
    }
}
