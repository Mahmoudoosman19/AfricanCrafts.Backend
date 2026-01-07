using IdentityHelper.Abstraction;
using Product.Application.Features.Sliders.Queries.GetListSlider;

namespace Product.Application.Features.Sliders.Queries.SliderDetalis
{
    internal class SliderDetalisQueryHandler : IQueryHandler<SliderDetalisQuery, SliderResponse>
    {
        private IGenericRepository<Domain.Entities.Slider> _sliderRepo;
        private readonly IMapper _mapper;
        private readonly IUserManagement _userManagement;

        public SliderDetalisQueryHandler(IGenericRepository<Domain.Entities.Slider> sliderRepo, IMapper mapper, IUserManagement userManagement)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
            _userManagement = userManagement;
        }

        public async Task<ResponseModel<SliderResponse>> Handle(SliderDetalisQuery request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepo.GetByIdAsync(request.Id);
            var sliderDto = _mapper.Map<SliderResponse>(slider); 
            return ResponseModel.Success(sliderDto);
        }
    }
}   