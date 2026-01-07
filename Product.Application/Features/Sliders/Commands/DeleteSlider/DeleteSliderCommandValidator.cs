using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.DeleteSlider
{
    public class DeleteSliderCommandValidator : AbstractValidator<DeleteSliderCommand>
    {
        public DeleteSliderCommandValidator(IGenericRepository<Slider> sliderRepo )
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .EntityExist(sliderRepo)
                .WithMessage(Messages.NotFound);
        }
    }
}
