using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Checkout.Command.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : ApiController
    {
        public CheckoutController(ISender sender):base(sender) { }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutOrderCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
       
    }
}
