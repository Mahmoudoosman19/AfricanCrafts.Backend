using Product.Domain.Abstraction;

namespace Product.Application.Features.Sliders.Commands.ManagSliderActivation
{
    public class ManageSliderActivationCommandValidator : AbstractValidator<ManageSliderActivationCommand>
    {
        public ManageSliderActivationCommandValidator(IProductRepository<Domain.Entities.Slider> sliderRepo) =>

            RuleFor(slider => slider.Id.Value)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(sliderRepo).WithMessage(Messages.NotFound);


    }
}
