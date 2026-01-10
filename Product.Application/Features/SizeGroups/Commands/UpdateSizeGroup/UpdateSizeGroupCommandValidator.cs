using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup;

public class UpdateSizeGroupCommandValidator : AbstractValidator<UpdateSizeGroupCommand>
{
    private readonly IProductRepository<SizeGroup> _sizeGroupRepository;

    public UpdateSizeGroupCommandValidator(
        IProductRepository<SizeGroup> sizeGroupRepository,
        IProductRepository<Size> sizeRepository)
    {
        _sizeGroupRepository = sizeGroupRepository;
        this.ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(Messages.EmptyField)
            .EntityExist(sizeGroupRepository)
            .WithMessage(Messages.NotFound);

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


        RuleFor(x => x.EditedSizes)
            .ForEachSetValidator(new UpdateSizeValidator(sizeRepository))
            .Custom(UniqueNames);

        RuleFor(x => x)
            .CustomAsync(NameExists);

        RuleFor(x => x.EditedQuestions)
            .ForEachSetValidator(new UpdateSizeGroupQuestionValidator());
    }

    private async Task NameExists(UpdateSizeGroupCommand request, ValidationContext<UpdateSizeGroupCommand> context,
        CancellationToken cancellationToken)
    {
        var nameExists = await _sizeGroupRepository.IsExistAsync
        (x => (x.NameAr == request.NameAr
               || x.NameEn == request.NameEn) && x.Id == Guid.Empty, cancellationToken);

        if (nameExists)
            context.AddFailure("Name", Messages.RedundantData);
    }

    private void UniqueNames(IEnumerable<UpdateSizeDto> sizes, ValidationContext<UpdateSizeGroupCommand> context)
    {
        bool namesArDistint = sizes.Select(x => x.NameAr)
            .Distinct()
            .Count() == sizes.Count();

        bool namesEnDistint = sizes.Select(x => x.NameEn)
            .Distinct()
            .Count() == sizes.Count();

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