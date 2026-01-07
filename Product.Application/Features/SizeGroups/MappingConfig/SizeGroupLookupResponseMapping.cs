using Mapster;
using Product.Application.Features.SizeGroups.Queries.GetSizeGroupsLookup;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.MappingConfig
{
    internal class SizeGroupLookupResponseMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SizeGroup, GetSizeGroupsLookupResponse>()
              .Map(dest => dest.Name, src => $"{src.NameAr} - {src.NameEn}");
        }
    }
}
