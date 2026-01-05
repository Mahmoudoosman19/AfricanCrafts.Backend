using Mapster;

namespace UserManagement.Application.Features.Auth.DashboardRole.MappingConfig
{
    public class DashboardRoleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.User, DashboardRoleResponse>()
              .Map(dest => dest.UserName, src => src.UserName)
              .Map(dest => dest.NameAr, src => src.FullNameAr ?? null)
              .Map(dest => dest.RoleNameAr, src => src.Role.NameAr ?? null)
              .Map(dest => dest.NameEn, src => src.FullNameEn ?? null)
              .Map(dest => dest.RoleNameEn, src => src.Role.NameEn ?? null);
        }
    }
}
