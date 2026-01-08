using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Product.Commands.AddProduct;
using Product.Application.Features.Product.Commands.ChangeProductActivation;
using Product.Application.Features.Product.Commands.RestockProduct;
using Product.Application.Features.Product.Commands.SuperVisorAddProduct;
using Product.Application.Features.Product.Commands.SupervisorProductAdmission;
using Product.Application.Features.Product.Commands.SupervisorRejectProduct;
using Product.Application.Features.Product.Commands.UpdateProduct;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Features.Product.Queries.GetProductDetailsCustomer;
using Product.Application.Features.Product.Queries.GetProducts;
using Product.Application.Features.Product.Queries.ProductStatisticsQuery;




namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class ProductController : ApiController
    {
        public ProductController(ISender sender) : base(sender) { }

        [HttpPost("add-product")]
        public async Task<IActionResult> Add([FromForm] AddProductCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return HandleResult(result);
        }

        [HttpPost("supervisor-add-product")]
        public async Task<IActionResult> SuperVisorAdd([FromForm] SuperVisorAddProductCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return HandleResult(result);
        }

        [HttpPost("update-product")]
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return HandleResult(result);
        }

        [HttpGet("get-products")]
        public async Task<IActionResult> Get([FromQuery] GetProductsByStatusAndVendorIdAndProductCodeAndNameWithImageQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);

            return HandleResult(result!);
        }

        [HttpGet("get-product-details")]
        public async Task<IActionResult> GetDetails([FromQuery] GetProductDetailsByIdWithRelationsProductQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);

            return HandleResult(result!);
        }

        [HttpPut("toggle-activation-product")]
        public async Task<IActionResult> ToggleActivation(Guid id, CancellationToken cancellationToken)
        {
            var changeProductActivationCommand = new ToggleProductActivationCommand()
            {
                ProductId = id,
            };

            var result = await Sender.Send(changeProductActivationCommand, cancellationToken);

            return HandleResult(result);
        }
        [HttpPost("restock-product")]
        public async Task<IActionResult> RestockProduct([FromForm] RestockProductCommand request, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(request, cancellationToken);

            return HandleResult(result);
        }
        [HttpPost("supervisor-product-admission")]
        public async Task<IActionResult> SupervisorProductAdmission([FromQuery] SupervisorProductAdmissionCommand request, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(request, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost("reject-product")]
        public async Task<IActionResult> RejectProduct([FromBody] SupervisorRejectProductCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await Sender.Send(request, cancellationToken);

            return HandleResult(result);
        }
       
      
        [HttpGet("product-customer-details")]
        public async Task<IActionResult> ProductDetails([FromQuery] CustomerGetProductDetailsByIdQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
        [HttpGet("get-product-statistics")]
        public async Task<IActionResult> GetProductStatistics([FromQuery] ProductStatisticsQuery query)
        {
            var respons = await Sender.Send(query);
            return Ok(respons);
        }
       
        
    }
}
    