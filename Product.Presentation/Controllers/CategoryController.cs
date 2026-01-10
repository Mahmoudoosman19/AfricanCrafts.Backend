using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Categories.Commands.ToggleCategoryActivation;
using Product.Application.Features.Categories.Commands.AddCategory;
using Product.Application.Features.Categories.Commands.UpdateCategory;
using Product.Application.Features.Categories.Queries.GetListCategory;
using Product.Application.Features.Categories.Queries.GetListCategoryLookup;
using Product.Application.Features.Categories.Queries.GetOneCategroy;
using Product.Application.Features.Categories.Queries.GetParentCategories;
using Product.Application.Features.Categories.Queries.GetSubCategories;
namespace Product.Presentation.Controllers;
using Common.Presentation.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Product.Application.Features.Categories.Queries.GetCategoriesForest;

[Route("api/[controller]")]
public sealed class CategoryController : ApiController
{
    public CategoryController(ISender sender) : base(sender)
    {

    }

    [HttpGet("MainCategories")]
    [AllowAnonymous]
    public async Task<IActionResult> MainCategories([FromQuery] GetParentCategoriesQuery request)
    {
        var response = await Sender.Send(request);

        return HandleResult(response);
    }


    [HttpGet("SubCategories")]
    [AllowAnonymous]
    public async Task<IActionResult> SubCategories([FromQuery] GetSubCategoriesQuery request)
    {
        var response = await Sender.Send(request);

        return HandleResult(response!);
    }
    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromForm] AddCategoryCommand updatedCategory, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(updatedCategory);
        return HandleResult(response);
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryCommand updatedCategory, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(updatedCategory);
        return HandleResult(response);
    }
    [HttpPut("activation-category")]
    public async Task<IActionResult> ActivationCategory(ToggleCategoryActivationCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        return HandleResult(result);

    }
    [HttpGet("Getcategory")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategory([FromQuery] GetCategoryByIdWithSizeGroupAndParentCategoryQuery request)
    {
        var response = await Sender.Send(request);
        return HandleResult(response);
    }
    [HttpGet("categoryList")]
    [AllowAnonymous]
    public async Task<IActionResult> GetcategoryList([FromQuery] GetCategoryByStatusWithFilterSearchQuery request)
    {
        var response = await Sender.Send(request);
        return HandleResult(response);
    }
    [HttpGet("categoryListLookup")]
    public async Task<IActionResult> GetcategoryListLookup()
    {
        var response = await Sender.Send(new GetListCategoryLookupQuery());
        return HandleResult(response);
    }
    [HttpGet("Get-Categories-Forest")]
    public async Task<IActionResult> GetCategoryCashing()
    {
        var response = await Sender.Send(new GetCategoriesForestQuery());
        return HandleResult(response);
    }
}
