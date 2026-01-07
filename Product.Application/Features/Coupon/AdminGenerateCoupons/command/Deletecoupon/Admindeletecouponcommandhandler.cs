namespace Product.Application.Features.Coupon.AdminGenerateCoupons.command.Deletecoupon
{
    internal class Admindeletecouponcommandhandler : ICommandHandler<Admindeletecouponcommand>
    {
        private readonly IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        private readonly IMapper _mapper;
        public Admindeletecouponcommandhandler(IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            _couponRepo = couponRepo;
        }
        public async Task<ResponseModel> Handle(Admindeletecouponcommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepo.GetByIdAsync(request.Id);
            
                _couponRepo.Delete(coupon!);
                await _couponRepo.SaveChangesAsync();
            return ResponseModel
                .Success(Messages.SuccessfulOperation);

        }
    }
}
