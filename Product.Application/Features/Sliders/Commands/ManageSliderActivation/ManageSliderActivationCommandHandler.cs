namespace Product.Application.Features.Sliders.Commands.ManagSliderActivation
{
    public class ManageSliderActivationCommandHandler:ICommandHandler<ManageSliderActivationCommand>    
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Slider> _sliderRepo;
        public ManageSliderActivationCommandHandler(IMapper mapper, IGenericRepository<Domain.Entities.Slider> sliderRepo)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
        }
   
        public async Task<ResponseModel> Handle(ManageSliderActivationCommand request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepo.GetByIdAsync(request.Id.Value);
            slider.SetActivation(!slider.IsActive);
            _sliderRepo.Update(slider);
            await _sliderRepo.SaveChangesAsync();
            return ResponseModel.Success();
        }
    }
}
