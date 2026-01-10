using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.TemplateSizes.Commands.DeleteSizeGroup
{
    internal class DeleteSizeGroupCommandValidator
        : AbstractValidator<DeleteSizeGroupCommand>
    {

        public DeleteSizeGroupCommandValidator(
            IProductRepository<SizeGroup> sizeGroupRepository)
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Messages.EmptyField);

            RuleFor(x => x.Id)
                .EntityExist(sizeGroupRepository)
                .WithMessage(Messages.NotFound);
        }
    }
}
