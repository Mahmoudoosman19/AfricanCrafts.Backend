using Mapster;
using Product.Application.Features.Sizes.Queries.GetSizesBySizeGroupId;
using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.MappingConfig
{
    internal class GetSizesMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Size, GetSizesResponse>()
              .Map(dest => dest.Name, src => $"{src.NameAr} - {src.NameEn}");
        }
    }
}
