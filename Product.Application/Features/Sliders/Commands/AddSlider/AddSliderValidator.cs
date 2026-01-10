using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.AddSlider
{

    public class AddSliderValidator : AbstractValidator<AddSliderCommand>
    {
        private readonly IProductRepository<Category> _CategoryRepo;
        private readonly IProductRepository<Slider> _SliderRepo;
        public AddSliderValidator(IProductRepository<Category> category, IProductRepository<Slider> sliderRepo)
        {
            _CategoryRepo = category;
            _SliderRepo = sliderRepo;   

            RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage(Messages.EmptyField).IsArabic().WithMessage(Messages.IncorrectData);
            RuleFor(x => x.NameEn)
               .NotEmpty().WithMessage(Messages.EmptyField).IsEnglish().WithMessage(Messages.IncorrectData) ;
            RuleFor(x => x.Image)
               .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x.CategoryId.Value)
                .EntityExist(_CategoryRepo).When(_Category => _Category.CategoryId.HasValue)
                .WithMessage(Messages.NotFound);
            RuleFor(x => x).CustomAsync(IsNameExist);
        }
        private async Task IsNameExist(AddSliderCommand request, ValidationContext<AddSliderCommand> context, CancellationToken cancellationToken)
        {
            var isExist = await _SliderRepo.IsExistAsync(x => x.NameAr == request.NameAr);
            if (isExist)
                context.AddFailure(nameof(request.NameAr), Messages.RedundantData);
            isExist = await _SliderRepo.IsExistAsync(x => x.NameEn == request.NameEn);
            if (isExist)
                context.AddFailure(nameof(request.NameEn), Messages.RedundantData);

        }

    }
}
