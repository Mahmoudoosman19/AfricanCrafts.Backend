using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Auth.Commands.Register.Abstract
{
    internal abstract class BaseRegister
    {
        protected readonly IMapper _mapper;
        protected readonly CustomUserManager _userManager;
        protected readonly IUserRepository<Role> _roleRepo;

        public BaseRegister(
            IMapper mapper,
            CustomUserManager userManager,
            IUserRepository<Role> roleRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepo = roleRepo;
        }

        public abstract RegisterType Type { get; set; }

        public abstract Task<ResponseModel> Register(RegisterCommand registerDto);
    }
}
