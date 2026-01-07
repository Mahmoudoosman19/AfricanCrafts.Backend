using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Color.Queries.GetColorsListLookup;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class ColorController : ApiController
    {
        public ColorController(ISender sender) : base(sender)
        {

        }
        [HttpGet("colorListLookup")]
        public async Task<IActionResult> GetColorListLookup()
        {
            var response = await Sender.Send(new GetColorsListLookupQuery());
            return HandleResult(response);
        }
    }
}
