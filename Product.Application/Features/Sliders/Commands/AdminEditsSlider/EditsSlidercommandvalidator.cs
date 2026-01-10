using Product.Application.Helpers;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sliders.Commands.AdminEditsSlider
{
    internal class EditsSlidercommandvalidator: AbstractValidator<EditsSlidercommand>
    {
        private readonly IProductRepository<Slider> _sliderRepo;
        private readonly IProductRepository<Category> _categoryRepo;    
        public EditsSlidercommandvalidator(IProductRepository<Slider> sliderRepo, IProductRepository<Category> categoryRepo)
        {
            _sliderRepo = sliderRepo;
            _categoryRepo = categoryRepo;
            ValidationRules();
        }
        private void ValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(_sliderRepo).WithMessage(Messages.NotFound);

            RuleFor(x => x).CustomAsync(IsNameExistforAnotherSliderAsync);
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MaximumLength(50)
                .IsArabic().WithMessage(Messages.IncorrectData);


            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MaximumLength(50)
                  .IsEnglish().WithMessage(Messages.IncorrectData); ;

            RuleFor(x => x).Custom(ValidateImage);

            RuleFor(x => x.CategoryId.Value)
                 .EntityExist(_categoryRepo).When(slider => slider.CategoryId.HasValue)
                 .WithMessage(Messages.NotFound);
        }

        private async Task IsNameExistforAnotherSliderAsync(EditsSlidercommand request, ValidationContext<EditsSlidercommand> context, CancellationToken cancellationToken)
        {
            var isExist = await _sliderRepo.IsExistAsync(x => x.NameAr == request.NameAr && x.Id != request.Id);
            if (isExist)
                context.AddFailure(nameof(request.NameAr), Messages.RedundantData);
            isExist = await _sliderRepo.IsExistAsync(x => x.NameEn == request.NameEn && x.Id != request.Id);
            if (isExist)
                context.AddFailure(nameof(request.NameEn), Messages.RedundantData);
        }

        private void ValidateImage(EditsSlidercommand request, ValidationContext<EditsSlidercommand> context)
        {
            if (request.Image != null && !ImageSetting.IsAllowedImageTypes(request.Image.FileName))
                context.AddFailure(nameof(request.Image), Messages.AllowedImageTypes.Replace("allTypes", ImageSetting.allowedImageTypes));

            if (request.Image != null && request.Image.Length > ImageSetting.MaxLegthByBytes(5))
                context.AddFailure(nameof(request.Image), Messages.MaxSizeFiles);
        }

    }
}

