namespace Product.Application.Features.Advertisements.Command.AddAdvertisement
{
    public class AdvertisementValidator : AbstractValidator<AddAdvertisementCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Advertisement> _advertisementRepository;
        public AdvertisementValidator(IGenericRepository<Domain.Entities.Advertisement> advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
            ValidationRules();
        }

        private void ValidationRules()
        {
            RuleFor(x => x).CustomAsync(IsNameExist);
            RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsArabic().WithMessage(Messages.IncorrectData);
            RuleFor(x => x.NameEn)
               .NotEmpty()
               .WithMessage(Messages.EmptyField)
               .IsEnglish().WithMessage(Messages.IncorrectData); 
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .Must(file => file != null && Path.GetExtension(file.FileName)?.ToLower() == ".jpg")
                .WithMessage("The image must have the extension jpg only.");
            RuleFor(x => x.DescriptionAr)
               .NotEmpty()
               .WithMessage(Messages.EmptyField)
               .IsArabic().WithMessage(Messages.IncorrectData); 
            RuleFor(x => x.DescriptionEn)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsEnglish().WithMessage(Messages.IncorrectData);

            RuleFor(x => x.AdvertisementUrl)
                .NotEmpty()
                .WithMessage(Messages.EmptyField);
               

        }
        private async Task IsNameExist(AddAdvertisementCommand request, ValidationContext<AddAdvertisementCommand> context, CancellationToken cancellationToken)
        {
            var isExist = await _advertisementRepository.IsExistAsync(x => x.NameAr == request.NameAr);
            if (isExist)
                context.AddFailure(nameof(request.NameAr), Messages.RedundantData);
            isExist = await _advertisementRepository.IsExistAsync(x => x.NameEn == request.NameEn);
            if (isExist)
                context.AddFailure(nameof(request.NameEn), Messages.RedundantData);


        }

    }
}
