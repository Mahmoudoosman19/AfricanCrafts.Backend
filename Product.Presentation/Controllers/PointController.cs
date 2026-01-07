using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Points.Queries;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class PointController : ApiController
    {
        public PointController(ISender sender) : base(sender)
        {

        }
        [HttpGet("get-points-lookup")]
        public async Task<IActionResult> GetPointsListLookup()
        {
            var response = await Sender.Send(new GetPointsListLookupQuery());
            return HandleResult(response);
        }
    }
}
