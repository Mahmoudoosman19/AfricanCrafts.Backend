using Mapster;
using Product.Application.Features.SizeGroups.Commands.CreateSizeGroup;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.MappingConfig
{
    internal class SizeGroupCreateCommandMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSizeGroupCommand, SizeGroup>()
                .Map(dest => dest.Sizes, src => src.Sizes);
        }
    }
}
