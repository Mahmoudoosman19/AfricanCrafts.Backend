using Product.Application.Features.Categories.Queries.GetCategroyById;
using Product.Application.Specifications.Categories;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.Queries.GetOneCategroy
{
    internal class GetCategoryByIdWithSizeGroupAndParentCategoryQueryHandler : IQueryHandler<GetCategoryByIdWithSizeGroupAndParentCategoryQuery, GetCategoryDetailsResponse>
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public GetCategoryByIdWithSizeGroupAndParentCategoryQueryHandler(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public Task<ResponseModel<GetCategoryDetailsResponse>> Handle(GetCategoryByIdWithSizeGroupAndParentCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _categoryRepo.GetEntityWithSpec(new GetCategoryByIdWithSizeGroupAndParentCategorySpecification(request.Id));
            var categoryResponse = _mapper.Map<GetCategoryDetailsResponse>(category!);
            return Task.FromResult(ResponseModel.Success(categoryResponse));
        }
    }
}
