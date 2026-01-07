

using Product.Application.Features.Categories.Queries.GetSubCategories;
using Product.Domain.Entities;

namespace Product.Application.Specifications.Categories;

internal class GetSubCategoriesSpecification : Specification<Category>
{
    public GetSubCategoriesSpecification(GetSubCategoriesQuery request)
    {
        AddCriteria(c => c.ParentId == request.Id);

        if (request.Name is not null)
            AddCriteria(c => c.NameAr.Contains(request.Name) || c.NameEn.Contains(request.Name));

        if (request.IsActive is not null)
            AddCriteria(c => c.IsActive == request.IsActive);

        ApplyPaging(request.PageSize, request.PageIndex);
    }
}
