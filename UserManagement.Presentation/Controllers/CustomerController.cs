using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Customer.Commands.AddBaseAvatarForCustomer;
using UserManagement.Application.Features.Customer.Commands.AddCustomizedAvatarForCustomer;
using UserManagement.Application.Features.Customer.Queries.CheckIfCustomerHasAvatar;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]
    public class CustomerController : ApiController
    {
        public CustomerController(ISender sender) : base(sender)
        {

        }
      
        [HttpPost("Add-Base-Avatar-For-Customer")]
        public async Task<IActionResult> AddBaseAvatarForCustomer([FromBody] AddBaseAvatarForCustomerCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpPost("Add-Customized-Avatar-For-Customer")]
        public async Task<IActionResult> AddCustomizedAvatarForCustomer([FromForm] AddCustomizedAvatarForCustomerCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }

        [HttpGet("Check-If-Customer-Has-Avatar")]
        public async Task<IActionResult> AddCustomizedAvatarForCustomer([FromQuery] CheckIfCustomerHasAvatarQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }


    }

}
