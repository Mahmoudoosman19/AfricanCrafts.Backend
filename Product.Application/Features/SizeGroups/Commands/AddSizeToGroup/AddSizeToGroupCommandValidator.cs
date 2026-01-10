using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.AddSizeToGroup
{
    public sealed class AddSizeToGroupCommandValidator
        : AbstractValidator<AddSizeToGroupCommand>
    {
        private readonly IProductRepository<Size> _sizeRepository;
        public AddSizeToGroupCommandValidator(
            IProductRepository<SizeGroup> sizeGroupRepository,
            IProductRepository<Size> sizeRepository)
        {
            _sizeRepository = sizeRepository;
            RuleFor(x => x.SizeGroupId)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .EntityExist(sizeGroupRepository)
                .WithMessage(Messages.NotFound);

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsArabic(combineNumbers: true)
                .WithMessage(Messages.IncorrectData);

            RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .IsEnglish(combineNumbers: true)
                .WithMessage(Messages.IncorrectData);

            RuleFor(x => x)
                .CustomAsync(NamesUnique);

            RuleFor(x => x.DescriptionAr)
                .IsArabic(combineNumbers: true)
                .WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrWhiteSpace(x.DescriptionAr));

            RuleFor(x => x.DescriptionEn)
                .IsEnglish(combineNumbers: true)
                .WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrWhiteSpace(x.DescriptionEn));
        }

        private async Task NamesUnique(AddSizeToGroupCommand command, ValidationContext<AddSizeToGroupCommand> validationContext, CancellationToken cancellationToken)
        {
            var unique = await _sizeRepository.IsExistAsync(s =>
                s.NameAr == command.NameAr && s.SizeGroupId == command.SizeGroupId 
                || s.NameEn == command.NameEn && s.SizeGroupId == command.SizeGroupId
                , cancellationToken);
            if (unique)
                validationContext.AddFailure("Name", Messages.RedundantData);
        }
    }
}