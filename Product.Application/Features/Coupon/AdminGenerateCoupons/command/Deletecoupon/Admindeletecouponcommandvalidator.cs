namespace Product.Application.Features.Coupon.AdminGenerateCoupons.command.Deletecoupon
{
    internal class Admindeletecouponcommandvalidator : AbstractValidator<Admindeletecouponcommand>
    {
        public Admindeletecouponcommandvalidator(IGenericRepository<Domain.Entities.Coupon> couponRepo )
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .EntityExist(couponRepo)
               .WithMessage(Messages.NotFound);
            
               
        }
    }
}
