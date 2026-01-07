using Product.Application.Specifications.GenarceSpecificationToHomePage;

namespace Product.Application.Features.HomePage.Query.Sliders
{
    internal class GetSliderQueryHandler : IQueryHandler<GetSliderQuery, IEnumerable<GetSliderQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Slider> _sliderRepo;
        private readonly IMapper _mapper;

        public GetSliderQueryHandler(IGenericRepository<Domain.Entities.Slider> sliderRepo, IMapper mapper)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<GetSliderQueryResponse>>> Handle(GetSliderQuery request, CancellationToken cancellationToken)
        {
            var sliderQuery = _sliderRepo.GetWithSpec(new SliderSpecification(request)).data.ToList();
            var sliderResponses = _mapper.Map<IEnumerable<GetSliderQueryResponse>>(sliderQuery);
            return ResponseModel.Success(sliderResponses, sliderQuery.Count);
        }
    }
}
