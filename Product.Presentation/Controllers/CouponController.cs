using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Coupon.AdminGenerateCoupons;
using Product.Application.Features.Coupon.AdminGenerateCoupons.command.Deletecoupon;
using Product.Application.Features.Coupon.ManageCouponActivation;
using Product.Application.Features.Coupon.Queries.GetCouponDetailsByCodeForCustomer;
using Product.Application.Features.Coupon.Queries.GetCouponDetailsById;
using Product.Application.Features.Coupon.Queries.ListCoupon;
using Product.Application.Features.Coupon.VendorGenerateCoupons;
using Product.Application.Features.Coupon.VendorManageCouponActivation;
using Product.Application.Features.Coupons.Query;


namespace Product.Presentation.Controllers
{

    [Route("api/[controller]")]
    public sealed class CouponController : ApiController
    {
        public CouponController(ISender sender) : base(sender)
        {
        }

        [HttpPost("Activation")]
        public async Task<IActionResult> Activation(ManageCouponActivationCommand request, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(request, cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("get-vender-list-coupons")]
        public async Task<IActionResult> GetVenderCoupons([FromQuery] GetCouponsByStatusAndDateQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
        [HttpGet("get-list-coupons")]
        public async Task<IActionResult> GetCoupons([FromQuery] GetAllCouponsQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
        [HttpPost("add-Coupon")]
        public async Task<IActionResult> Post(CreateCouponCommand couponsCommand)
        {
            return HandleResult(await Sender.Send(couponsCommand));
        }

        [HttpGet("Get-Coupon")]
        public async Task<IActionResult> GetCouponDetails([FromQuery] GetCouponDetailsQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
        [HttpGet("Get-Coupon-Details-By-Code")]
        public async Task<IActionResult> GetCouponDetailsByCode([FromQuery] GetCouponDetailsByCodeForCustomerQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
        [HttpDelete("delete-coupon")]
        public async Task<IActionResult> Deletecoupon([FromQuery] Admindeletecouponcommand deleteCoupon, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(deleteCoupon, cancellationToken);
            return HandleResult(response);
        }
        [HttpDelete("vendor-get-coupon")]
        public async Task<IActionResult> VendorGetCoupon([FromQuery] GetCouponsByStatusAndDateQuery deleteCoupon, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(deleteCoupon, cancellationToken);
            return HandleResult(response);
        }
        [HttpPost("vendor-Activation")]
        public async Task<IActionResult> VendorActivation(VendorManageCouponActivationCommand request, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(request, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost("Vendor-Add-Coupon")]
        public async Task<IActionResult> VendorAddCoupon(VendorGenerateCouponsCommand couponsCommand)
        {
            return HandleResult(await Sender.Send(couponsCommand));
        }
    }
}
