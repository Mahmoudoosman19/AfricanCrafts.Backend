using Product.Domain.Entities;

namespace Product.Application.Features.Points.Queries
{
    internal class GetPointsListLookupQueryHandler : IQueryHandler<GetPointsListLookupQuery, IReadOnlyList<PointLookupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Point> _pointRepo;

        public GetPointsListLookupQueryHandler(IMapper mapper, IGenericRepository<Point> pointRepo)
        {
            _mapper = mapper;
            _pointRepo = pointRepo;
        }
        public Task<ResponseModel<IReadOnlyList<PointLookupResponse>>> Handle(GetPointsListLookupQuery request, CancellationToken cancellationToken)
        {
            var points = _pointRepo.Get();
            var mappingPoints = _mapper.Map<IReadOnlyList<PointLookupResponse>>(points);
            return Task.FromResult(ResponseModel.Success(mappingPoints));
        }
    }
}
