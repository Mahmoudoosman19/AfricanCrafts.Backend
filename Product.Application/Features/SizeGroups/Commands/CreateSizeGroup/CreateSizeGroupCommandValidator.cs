using Product.Application.SharedDTOs.SizeGroup;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.CreateSizeGroup
{
    public sealed class CreateSizeGroupCommandValidator
        : AbstractValidator<CreateSizeGroupCommand>
    {
        private readonly IProductRepository<SizeGroup> _sizeGroupRepository;
        public CreateSizeGroupCommandValidator(
            IProductRepository<SizeGroup> sizeGroupRepository,
            IProductRepository<Size> sizeRepository)
        {
            _sizeGroupRepository = sizeGroupRepository;

            this.ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(ContainsLetters).WithMessage(Messages.IncorrectData)
                .IsArabic(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.NameAr),
                    ApplyConditionTo.CurrentValidator);
            
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(ContainsLetters).WithMessage(Messages.IncorrectData)
                .IsEnglish(combineNumbers: true).WithMessage(Messages.IncorrectData)
                .When(x => !string.IsNullOrEmpty(x.NameEn),
                    ApplyConditionTo.CurrentValidator);

            RuleFor(x => x)
                .CustomAsync(NameExists);

            RuleFor(x => x.Sizes)
                .ForEachSetValidator(new SizeCommandValidator(sizeRepository));

            RuleFor(x => x.Questions)
                .ForEachSetValidator(new SizeGroupQuestionDtoValidator());

            RuleFor(x => x.Sizes)
                .Custom(UniqueNames);
        }

        private async Task NameExists(CreateSizeGroupCommand request, ValidationContext<CreateSizeGroupCommand> context, CancellationToken cancellationToken)
        {
            var nameExists = await _sizeGroupRepository.IsExistAsync
                (x => x.NameAr == request.NameAr
                || x.NameEn == request.NameEn, cancellationToken);
            if (nameExists)
                context.AddFailure("name", Messages.RedundantData);
        }

        private void UniqueNames(ICollection<CreateSizeDto> sizes, ValidationContext<CreateSizeGroupCommand> context)
        {
            bool namesArDistint = sizes.Select(x => x.NameAr)
                .Distinct()
                .Count() == sizes.Count;

            bool namesEnDistint = sizes.Select(x => x.NameEn)
                .Distinct()
                .Count() == sizes.Count;

            if (!namesArDistint)
                context.AddFailure(nameof(sizes), Messages.DuplicateDataInList);

            if (!namesEnDistint)
                context.AddFailure(nameof(sizes), Messages.DuplicateDataInList);
        }
        private bool ContainsLetters(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return name.Any(char.IsLetter);
        }
    }
}
