namespace Product.Application.Features.Coupon.VendorGenerateCoupons
{
    public class VendorGenerateCouponsCommandValidator : AbstractValidator<VendorGenerateCouponsCommand>
	{
		public VendorGenerateCouponsCommandValidator(
			IGenericRepository<Domain.Entities.Coupon> _couponRepo
			)
		{
            RuleFor(command => command.StartDate)
     .NotEmpty().WithMessage("Start Date is required.");

            RuleFor(command => command.ExpireDate)
                .NotEmpty().WithMessage("Expire Date is required.")
                .GreaterThan(command => command.StartDate)
                .WithMessage("Start Date cannot be after Expire Date.");

            RuleFor(Coupon => Coupon.DiscountPercentage)
				.NotEmpty().WithMessage(Messages.EmptyField)
				.WithMessage(Messages.NotFound);

			RuleFor(Coupon => Coupon.ExpireDate)
				.NotEmpty().WithMessage(Messages.EmptyField)
				.WithMessage(Messages.NotFound);

			RuleFor(Coupon => Coupon.StartDate)
				.NotEmpty().WithMessage(Messages.EmptyField)
				.WithMessage(Messages.NotFound);

			RuleFor(Coupon => Coupon.UserCount)
				.NotEmpty().WithMessage(Messages.EmptyField)
				.WithMessage(Messages.NotFound);

			RuleFor(Coupon => Coupon.NumOfCoupons)
				.GreaterThanOrEqualTo(1).WithMessage(Messages.MinNumOfCoupon);
		}
	}
}
