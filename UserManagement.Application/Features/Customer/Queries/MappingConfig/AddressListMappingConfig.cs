using Mapster;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Customer.Queries.MappingConfig
{
    public class AddressListMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Address, CustomerAddressQueryResponse>()
              .Map(dest => dest.Address, src => src.AddressName)
              .Map(dest => dest.Floor, src => src.Floor ?? null);

        }
    }
}
