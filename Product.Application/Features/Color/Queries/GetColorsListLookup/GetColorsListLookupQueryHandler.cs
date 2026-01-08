using Product.Domain.Abstraction;

namespace Product.Application.Features.Color.Queries.GetColorsListLookup
{
    internal class GetColorsListLookupQueryHandler : IQueryHandler<GetColorsListLookupQuery, IReadOnlyList<ColorLookupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Domain.Entities.Color> _colorRepo;
        public GetColorsListLookupQueryHandler(IMapper mapper, IProductRepository<Domain.Entities.Color> colorRepo)
        {
            _mapper = mapper;
            _colorRepo = colorRepo;
        }
        public Task<ResponseModel<IReadOnlyList<ColorLookupResponse>>> Handle(GetColorsListLookupQuery request, CancellationToken cancellationToken)
        {
            var colors = _colorRepo.Get();
            var mappingColors = _mapper.Map<IReadOnlyList<ColorLookupResponse>>(colors);
            return Task.FromResult(ResponseModel.Success(mappingColors));
        }
    }
}
