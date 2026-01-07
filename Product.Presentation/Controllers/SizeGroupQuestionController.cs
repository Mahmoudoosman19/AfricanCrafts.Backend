using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.SizeGroupQuestions.Commands.DeleteQuestions;
using Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion;
using Product.Application.Features.SizeTable.Commands.CreatesSizeTable;


namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class SizeGroupQuestionController : ApiController
    {
        public SizeGroupQuestionController(ISender sender) : base(sender)
        {
        }

        [HttpPost("add-questions")]
        public async Task<IActionResult> Createsquestions(CreatesQuestionsCommand CreatCommand)
        {
            var respons = await Sender.Send(CreatCommand);
            return Ok(respons);
        }

        [HttpDelete("delete-questions")]
        public async Task<IActionResult> Deletequestion([FromQuery] DeleteQuestionCommand deleteQuestion, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(deleteQuestion, cancellationToken);
            return HandleResult(response);
        }
        [HttpGet("get-list-questions")]
        public async Task<IActionResult> Getquestions([FromQuery] GetListSizeQuestionQuery GetQuery)
        {
            var respons = await Sender.Send(GetQuery);
            return Ok(respons);
        }
    }
}
