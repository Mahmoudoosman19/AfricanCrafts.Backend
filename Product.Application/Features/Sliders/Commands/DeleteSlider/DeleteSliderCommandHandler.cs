using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.DeleteSlider
{
    public class DeleteSliderCommandHandler : ICommandHandler<DeleteSliderCommand>
    {
        private readonly IProductRepository<Slider> _sliderRepo;
        private readonly IMapper _mapper;

        public DeleteSliderCommandHandler(IProductRepository<Slider> sliderRepo)
        {
            _sliderRepo = sliderRepo;   
        }
        public async Task<ResponseModel> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
        {
            var Slider = await _sliderRepo.GetByIdAsync(request.Id);
            _sliderRepo.Delete(Slider);
            await _sliderRepo.SaveChangesAsync();

            return ResponseModel
                .Success(Messages.SuccessfulOperation);
        }
    }
}
