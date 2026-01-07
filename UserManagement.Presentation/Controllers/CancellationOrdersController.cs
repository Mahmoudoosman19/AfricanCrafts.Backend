using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Customer.Commands.AddRefundProofImg;
//using UserManagement.Application.Features.OrderUser.Queries.GetPendingCancellationOrders;


namespace UserManagement.Presentation.Controllers
{

    [Route("api/UserManagement/[controller]")]
    public class CancellationOrdersController : ApiController
    {
        public CancellationOrdersController(ISender sender) : base(sender)
        {
        }

        //[HttpGet("Get-All-Pending-Cancellation-Orders")]
        //public async Task<IActionResult> GetAllPendingCancellationOrders([FromQuery] GetPendingCancellationOrdersQuery query, CancellationToken cancellationToken)
        //{
        //    var response = await Sender.Send(query);
        //    return HandleResult(response);
        //}

        [HttpPost("Add-Refund-Proof")]
        public async Task<IActionResult> AddRefundProof([FromForm] AddRefundProofImgCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }


      
    }
   
}
