using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Sizes.Commands.ToggleSizeStop;
using Product.Application.Features.Sizes.Queries.GetSizes;
using Product.Application.Features.Sizes.Queries.GetSizesBySizeGroupId;

namespace Product.Presentation.Controllers
{

    [Route("api/[controller]")]
    public sealed class SizeController : ApiController
    {
        public SizeController(ISender sender) : base(sender)
        {
        }

        [HttpPut("toggle-size-activation")]
        public async Task<IActionResult> ToggleStop(ToggleSizeActivationCommand command)
        {
            var response = await Sender.Send(command);
            return Ok(response);
        }
        [HttpGet("get-sizes")]
        public async Task<IActionResult> GetSizes([FromQuery] GetSizesBySizeGroupIdQuery request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }
        [HttpGet("get-List-Of-Sizes")]
        public async Task<IActionResult> GetListOfSizes([FromQuery] GetSizesByStatusQuery request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }

    }
}
