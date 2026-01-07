using Product.Application.Features.Categories.Queries.GetParentCategories;

namespace Product.Application.Specifications.Categories;

internal class GetParentCategoriesSpecification : Specification<Domain.Entities.Category>
{
    public GetParentCategoriesSpecification(GetParentCategoriesQuery request)
    {
        AddCriteria(c => c.ParentId == null);

        if(request.Name is not null)
            AddCriteria(c => c.NameAr.Contains(request.Name) || c.NameEn.Contains(request.Name));   

        if(request.IsActive is not null)
            AddCriteria(c => c.IsActive ==  request.IsActive);

        ApplyPaging(request.PageSize, request.PageIndex);
    }
}
