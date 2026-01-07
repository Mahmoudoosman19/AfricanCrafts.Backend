namespace Product.Application.Features.Coupon.ManageCouponActivation
{
    public class ManageCouponActivationHandler : ICommandHandler<ManageCouponActivationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.Coupon> _couponRepo;
        public ManageCouponActivationHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Coupon> couponRepo)
        {
            _couponRepo = couponRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel> Handle(ManageCouponActivationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _couponRepo.GetByIdAsync(request.Id);
               
                       
                if (coupon.IsActive == false && coupon.StartDate <= DateTime.Now && coupon.ExpireDate >= DateTime.Now)
                {
                    coupon.SetIsManuallyDeactivated(true);
                }

                coupon!.SetActivation(!coupon.IsActive);
                _couponRepo.Update(coupon);
                await _couponRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
