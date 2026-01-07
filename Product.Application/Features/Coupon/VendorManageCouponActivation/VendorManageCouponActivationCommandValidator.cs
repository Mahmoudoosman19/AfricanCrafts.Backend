namespace Product.Application.Features.Coupon.VendorManageCouponActivation
{
    public class VendorManageCouponActivationCommandValidator : AbstractValidator<VendorManageCouponActivationCommand>
    {
        public VendorManageCouponActivationCommandValidator(IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(couponRepo).WithMessage(Messages.NotFound);

            RuleFor(command => command)
                .MustAsync(async (command, cancellationToken) =>
                {
                    var coupon = await couponRepo.GetByIdAsync(command.Id);
                    return coupon != null && coupon.StartDate <= DateTime.Now;
                })
                .WithMessage("Coupon is not active right now.");
        }
    }
}

