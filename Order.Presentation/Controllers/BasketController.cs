using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Order.Application.Features.Basket.Command.DeleteBasket;
using Order.Application.Features.Basket.Command.MigrateBasket;
using Order.Application.Features.Basket.Command.RemoveItemFromBasket;
using Order.Application.Features.Basket.Command.UpdateBasket;
using Order.Application.Features.Basket.Command.UpdateItemQuantity;
using Order.Application.Features.Basket.Query.GetBasketCount;
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
        [HttpDelete("deleteBasket")]
        public async Task<IActionResult> DeleteBasket([FromQuery]DeleteBasketCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> RemoveItem([FromQuery]RemoveItemFromBasketCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        [HttpPatch("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromQuery] UpdateItemQuantityCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        [HttpGet("GetBasketCount")]
        public async Task<IActionResult> GetBasketCount([FromQuery] GetBasketCountQuery command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        // after frontend login
        [HttpPost("migrate")]
        public async Task<IActionResult> MigrateBasket([FromQuery] MigrateBasketCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
    }
}
