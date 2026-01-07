using Mapster;
using Product.Application.Features.Points.Queries;
using Product.Domain.Entities;

namespace Product.Application.Features.Points.MappingConfig
{
    internal class PointLookupMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Point, PointLookupResponse>()
              .Map(dest => dest.Name, src => $"{src.NameAr} - {src.NameEn}");
        }
    }
}
