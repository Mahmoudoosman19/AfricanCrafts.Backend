using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Queries.GetSizeGroups
{
    internal class GetSizeGroupsSpecification : Specification<SizeGroup>
    {
        public GetSizeGroupsSpecification(GetSizeGroupsQuery query)
        {
            if (query.NameSearch is not null)
                AddCriteria(s => s.NameAr.Contains(query.NameSearch) || s.NameEn.Contains(query.NameSearch));
            ApplyPaging(query.PageSize, query.PageIndex);
        }
    }
}
