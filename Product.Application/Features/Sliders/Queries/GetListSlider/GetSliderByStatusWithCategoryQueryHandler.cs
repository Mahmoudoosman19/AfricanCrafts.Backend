using Product.Application.Specifications.slider;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Sliders.Queries.GetListSlider
{
    public class GetSliderByStatusWithCategoryQueryHandler:IQueryHandler<GetSliderByStatusWithCategoryQuery,IEnumerable<SliderResponse>>
    {
        private readonly IProductRepository<Domain.Entities.Slider> _sliderRepo;
        private readonly IMapper _mapper;


        public GetSliderByStatusWithCategoryQueryHandler(IProductRepository<Domain.Entities.Slider> repository, IMapper mapper)
        {
            _sliderRepo = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IEnumerable<SliderResponse>>> Handle(GetSliderByStatusWithCategoryQuery request, CancellationToken cancellationToken)

        {
            var (sliderQuery, count) = _sliderRepo.GetWithSpec(new GetSliderByStatusWithCategorySpecification(request));
            var slider = _mapper.Map<IEnumerable<SliderResponse>>(sliderQuery);
            return ResponseModel.Success(slider, count);


        }
    }
}
