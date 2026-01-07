using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Review.Commands.AddReview;
using Product.Application.Features.Review.Commands.UpdateReview;
using Product.Application.Features.Review.Queries.GetProductReviews;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class ReviewController : ApiController
    {
        public ReviewController(ISender sender) : base(sender)
        {

        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview(AddReviewCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpPut("Update-review")]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }
        [HttpGet("get-review")]
        public async Task<IActionResult> GetProductReviews([FromQuery] GetProductReviewsQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }
    }
}
