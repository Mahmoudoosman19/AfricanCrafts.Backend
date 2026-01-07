using Mapster;
using Product.Application.Features.Color.Queries.GetColorsListLookup;

namespace Product.Application.Features.Color.MappingConfig
{
    internal class ColorLookupMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Color, ColorLookupResponse>()
              .Map(dest => dest.Name, src => $"{src.NameAr} - {src.NameEn}");
        }
    }
}
