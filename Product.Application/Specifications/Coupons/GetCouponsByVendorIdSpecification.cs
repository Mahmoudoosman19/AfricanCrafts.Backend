namespace Product.Application.Specifications.Coupons
{
    public class GetCouponsByVendorIdSpecification : Specification<Domain.Entities.Coupon>
	{
		public GetCouponsByVendorIdSpecification(Guid vendorId)
		{
			if (vendorId != Guid.Empty)
			{
				AddCriteria(coupon => coupon.UserId == vendorId);
			}
		}
	}
}
