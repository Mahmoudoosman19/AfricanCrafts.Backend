using Mapster;
using UserManagement.Application.Features.User.Queries.GetUserData;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.User.MappingConfig
{
    internal class GetUserDataResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.User, GetUserDataQueryResponse>()
                .Map(dest => dest.Role, src => src.Role == null ? string.Empty : src.Role.NameEn)
                .Map(dest => dest.Status, src => Enum.GetName(typeof(UserStatus), src.Status));
        }


    }
}
