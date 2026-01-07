namespace Product.Application.Features.Coupon.AdminGenerateCoupons
{
    public class AdminGenerateCouponsValidator : AbstractValidator<CreateCouponCommand>
    {
        public AdminGenerateCouponsValidator(
            IGenericRepository<Domain.Entities.Coupon> _couponRepo
            )
        {
            RuleFor(command => command.StartDate)
    .NotEmpty().WithMessage(Messages.StartDateisrequired);

            RuleFor(command => command.ExpireDate)
                .NotEmpty().WithMessage(Messages.ExpireDateisrequired)
                
                .GreaterThan(command => command.StartDate)
                .WithMessage(Messages.StartDatecannotbeafterExpireDate);

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
