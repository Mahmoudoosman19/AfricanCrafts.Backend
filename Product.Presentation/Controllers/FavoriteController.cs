using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Favorite.Command.ToggelFavorit;
using Product.Application.Features.Favorite.Qeury.ListFavorite;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class FavoriteController : ApiController
    {
        


        public FavoriteController(ISender sender) : base(sender)
        {

        }
        [HttpPost("Toggel-Favorite")]
        public async Task<IActionResult> ToggelFavorite([FromQuery] ToggelFavoriteCommand favorite)
        {
            var response = await Sender.Send(favorite);
            return HandleResult(response);
        }
        [HttpGet("Get-List-favorite")]
        public async Task<IActionResult> Getfavorite([FromQuery] ListFavoriteQeury request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request);
            return HandleResult(response);
        }



    }
}

