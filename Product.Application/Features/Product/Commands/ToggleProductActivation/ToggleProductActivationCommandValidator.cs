using IdentityHelper.Abstraction;
using Product.Application.Helpers;

namespace Product.Application.Features.Product.Commands.ChangeProductActivation;

internal class ToggleProductActivationCommandValidator
    : AbstractValidator<ToggleProductActivationCommand>
{
    private readonly ITokenExtractor _currentUser;
    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
    public ToggleProductActivationCommandValidator(
        IGenericRepository<Domain.Entities.Product> productRepo,
        ITokenExtractor currentUser)
    {
        _currentUser = currentUser;
        _productRepo = productRepo;
        
        RuleFor(x => x.ProductId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .EntityExist(productRepo).WithMessage(Messages.NotFound)
            .MustAsync(VerifyUser)
            .WithMessage(Messages.IncorrectData);
    }

    private async Task<bool> VerifyUser(Guid productId, CancellationToken cancellationToken)
    {
        var userRole = _currentUser.GetUserRole();
        if (userRole != UserRoles.Vendor)
            return true;
        
        var userId = _currentUser.GetUserId();
        var owner = await _productRepo.IsExistAsync
            (p =>  p.Id == productId && p.VendorId == userId, cancellationToken);

        return owner;
    }
}
