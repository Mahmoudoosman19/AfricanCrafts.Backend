namespace Product.Application.Features.Coupon.ManageCouponActivation
{
    public class ManageCouponActivationCommandValidator : AbstractValidator<ManageCouponActivationCommand>
    {
        public ManageCouponActivationCommandValidator(IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            RuleFor(coupon => coupon.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EntityExist(couponRepo).WithMessage(Messages.NotFound);

            RuleFor(coupon => coupon)
                .MustAsync(async (command, cancellation) =>
                {
                    var coupon = await couponRepo.GetByIdAsync(command.Id);
                    return coupon != null && coupon.StartDate <= DateTime.Now;
                })
                .WithMessage("Coupon is not active yet.");
        }
    }
}
