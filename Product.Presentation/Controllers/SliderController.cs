using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Sliders.Commands.AddSlider;
using Product.Application.Features.Sliders.Commands.AdminEditsSlider;
using Product.Application.Features.Sliders.Commands.DeleteSlider;
using Product.Application.Features.Sliders.Commands.ManagSliderActivation;
using Product.Application.Features.Sliders.Queries.GetListSlider;
using Product.Application.Features.Sliders.Queries.SliderDetalis;


namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]

    public sealed class SliderController : ApiController
    {
        public SliderController(ISender sender) : base(sender)
        {
        }

        [HttpGet("get-list-slider")]
        public async Task<IActionResult> CreateSlider([FromQuery] GetSliderByStatusWithCategoryQuery getlistSlider)
        {
            var response = await Sender.Send(getlistSlider);
            return HandleResult(response);
        }
        [HttpPut("Toggel-Activation-slider")]
        public async Task<IActionResult> Action(ManageSliderActivationCommand request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }
        [HttpPost("add-slider")]
        public async Task<IActionResult> CreateSlider([FromForm] AddSliderCommand addSlider)
        {
            var response = await Sender.Send(addSlider);
            return Ok(response);
        }
        [HttpDelete("delete-slider")]
        public async Task<IActionResult> Deleteslider([FromQuery] DeleteSliderCommand deleteSlider, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(deleteSlider , cancellationToken);
            return HandleResult(response);
        }
        [HttpPost("update-slider")]
        public async Task<IActionResult> UpdateSlider([FromForm] EditsSlidercommand updateslider, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(updateslider);
            return HandleResult(response);}
        [HttpGet("slider-detalis")]
        public async Task<IActionResult> SliderDetails([FromQuery] SliderDetalisQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
    }
}
