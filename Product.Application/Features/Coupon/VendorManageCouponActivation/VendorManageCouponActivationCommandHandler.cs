using IdentityHelper.Abstraction;

namespace Product.Application.Features.Coupon.VendorManageCouponActivation
{
    public class ManageCouponActivationHandler : ICommandHandler<VendorManageCouponActivationCommand>
	{
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Domain.Entities.Coupon> _couponRepo;
		private readonly ITokenExtractor _tokenExtractor;

		public ManageCouponActivationHandler(
			IMapper mapper,
			IGenericRepository<Domain.Entities.Coupon> couponRepo,
			ITokenExtractor tokenExtractor)
		{
			_couponRepo = couponRepo;
			_mapper = mapper;
			_tokenExtractor = tokenExtractor;
		}

		public async Task<ResponseModel> Handle(VendorManageCouponActivationCommand request, CancellationToken cancellationToken)
		{
            var vendorId = _tokenExtractor.GetUserId();
            //    var specification = new GetCouponsByVendorIdSpecification(vendorId);
            var coupon = await _couponRepo.GetByIdAsync(request.Id);

            if (coupon.IsActive == false && coupon.StartDate <= DateTime.Now && coupon.ExpireDate >= DateTime.Now)
            {
                coupon.SetIsManuallyDeactivated(true);
            }

            if (coupon == null || coupon.UserId != vendorId)
            {
                return ResponseModel.Failure(Messages.NotFound);
            }

            coupon.SetActivation(!coupon.IsActive);
            _couponRepo.Update(coupon);
            await _couponRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);

        }
    }
}
