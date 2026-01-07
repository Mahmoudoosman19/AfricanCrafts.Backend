using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.HomePage.Query.Categories;
using Product.Application.Features.HomePage.Query.Sliders;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class HomePageController : ApiController
    {
        public HomePageController(ISender sender) : base(sender)
        {

        }
        [HttpGet("Get-Sliders")]
        public async Task<IActionResult> GetSlider([FromQuery] GetSliderQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response!);
        }

        [HttpGet("Get-Category")]
        public async Task<IActionResult> GetCategory([FromQuery] GetCategoriesQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response!);
        }
    }
}
