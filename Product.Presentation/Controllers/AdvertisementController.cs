using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Advertisement.Command.ManageAdvertisementActivation;
using Product.Application.Features.Advertisements.Command.AddAdvertisement;
using Product.Application.Features.Advertisements.Queries.CustomerGetAllAdvertisment;
using Product.Application.Features.Advertisements.Queries.GetAllAdvertisment;
using Product.Application.Features.Advertisements.Queries.GetList;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class AdvertisementController : ApiController
    {
        public AdvertisementController(ISender sender) : base(sender)
        {
        }
        [HttpPost("add-Advertisement")]
        public async Task<IActionResult> CreateAdvertisement([FromForm] AddAdvertisementCommand Advertisement)
        {
            var response = await Sender.Send(Advertisement);
            return HandleResult(response);
        }
        [HttpPut("Toggel-Activation")]
        public async Task<IActionResult> Action(ManageAdvertisementActivationCommand request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }
        [HttpGet("Get-List-Advertisement")]
        public async Task<IActionResult> GetAdvertisement([FromQuery] GetAdvertisementBaseStatusAndRoleQuery request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }
        [HttpGet("Get-All-Advertisement-image")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAdvertisementImage([FromQuery] GetAllAdvertisementImageByStatusQuery request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }

        [HttpGet("Customer-Get-List-Advertisement")]
        [AllowAnonymous]
        public async Task<IActionResult> CustomerGetAdvertisement([FromQuery] CustomerGetAdvertisementByStatusQuery request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }
    }
}
