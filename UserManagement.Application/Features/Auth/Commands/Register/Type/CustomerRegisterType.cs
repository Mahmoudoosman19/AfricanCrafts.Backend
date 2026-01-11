using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Abstraction;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.Register.Type
{
    internal class CustomerRegisterType : BaseRegister
    {
        private readonly IUserRepository<Domain.Entities.Customer> _customerRepo;

        public CustomerRegisterType(
            IMapper mapper,
            CustomUserManager userManager,
            IUserRepository<Role> roleRepo,
            IUserRepository<Domain.Entities.Customer> customerRepo
            )
            : base(
                  mapper,
                  userManager,
                  roleRepo)
        {
            _customerRepo = customerRepo;
        }

        public override RegisterType Type { get; set; } = RegisterType.Customer;

        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
        {
            var user = _mapper.Map<Domain.Entities.User>(registerDto.Customer!);

            user.SetStatus(UserStatus.Active);
            var customerRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Customer.ToString()));
            user.AssignRole(customerRole!.Id);
            user.ConfirmEmail();
            user.ConfirmPhoneNumber();
            var result = await _userManager.CreateAsync(user, registerDto.Customer!.Password);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));

                return ResponseModel.Failure(errors);
            }

            return ResponseModel.Success();
        }
    }
}
