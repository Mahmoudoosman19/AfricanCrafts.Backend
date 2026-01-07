using Mapster;
using Product.Application.Features.Categories.Queries.GetListCategoryLookup;
using Product.Domain.Entities;

namespace Product.Application.Features.Categories.MappingConfig
{
    internal class CategoryLookUpResponseMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, GetCategorylookupRespone>()
              .Map(dest => dest.Name, src => $"{src.NameAr} - {src.NameEn}");
        }
    }
}
