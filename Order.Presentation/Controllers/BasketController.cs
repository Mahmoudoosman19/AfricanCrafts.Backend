using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Order.Application.Features.Basket.Command.UpdateBasket;
using Order.Application.Features.Basket.Query.GetBasketQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : ApiController
    {
        public BasketController(ISender sender)
            :base(sender)
        {
            
        }

        [HttpGet("GetBasket")]
        public async Task<IActionResult> GetBasket([FromQuery]GetBasketQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }
        [HttpPost("updateBasket")]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
    }
}
