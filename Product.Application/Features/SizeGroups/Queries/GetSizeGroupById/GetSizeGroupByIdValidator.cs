using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroupById
{
    internal sealed class GetSizeGroupByIdValidator : AbstractValidator<GetSizeGroupByIdQuery>
    {
        public GetSizeGroupByIdValidator(
            IProductRepository<SizeGroup> sizeGroupRepo)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .EntityExist(sizeGroupRepo)
                .WithMessage(Messages.NotFound);
        }
    }
}
