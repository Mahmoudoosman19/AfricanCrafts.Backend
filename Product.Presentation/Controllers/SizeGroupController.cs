using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.SizeGroups.Commands.AddSizeToGroup;
using Product.Application.Features.SizeGroups.Commands.CreateSizeGroup;
using Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup;
using Product.Application.Features.SizeGroups.Queries.GetSizeGroupById;
using Product.Application.Features.SizeGroups.Queries.GetSizeGroups;
using Product.Application.Features.SizeGroups.Queries.GetSizeGroupsLookup;
using Product.Application.Features.TemplateSizes.Commands.DeleteSizeGroup;


namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class SizeGroupController : ApiController
    {
        public SizeGroupController(ISender sender) : base(sender)
        {
        }

        [HttpPost("add-size-group")]
        public async Task<IActionResult> CreateSizeGroup(
            CreateSizeGroupCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return HandleResult(response);
        }

        [HttpPut("add-size-to-sizegroup")]
        public async Task<IActionResult> AddSizeToGroup(
            AddSizeToGroupCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return HandleResult(response);
        }

        [HttpGet("get-size-group-details")]
        public async Task<IActionResult> GetSizeGroupById(
                       [FromQuery] GetSizeGroupByIdQuery query)
        {
            var response = await Sender.Send(query, new CancellationToken());

            return HandleResult(response);
        }

        [HttpGet("get-size-groups")]
        public async Task<IActionResult> GetSizeGroups(
                       [FromQuery] GetSizeGroupsQuery query)
        {
            var response = await Sender.Send(query, new CancellationToken());

            return HandleResult(response);
        }

        [HttpPut("edit-size-group")]
        public async Task<IActionResult> EditSizeGroup(
            UpdateSizeGroupCommand command)
        {
            var response = await Sender.Send(command);

            return HandleResult(response);
        }

        [HttpDelete("delete-size-group")]
        public async Task<IActionResult> ToggleDeleteSizeGroupById(
            [FromQuery] DeleteSizeGroupCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return HandleResult(response);
        }

        [HttpGet("get-size-groups-lookup")]
        public async Task<IActionResult> GetSizeGroupsLookUp()
        {
            var response = await Sender.Send(new GetSizeGroupsLookupQuery(), new CancellationToken());

            return HandleResult(response);
        }
    }
}
