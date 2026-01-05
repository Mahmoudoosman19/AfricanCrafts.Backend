using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Validators
{
    public class ICloudLoginDtoValidator : AbstractValidator<ICloudLoginDto>
    {
        public ICloudLoginDtoValidator()
        {
            RuleFor(x => x.AuthorizationCode)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
    }
}
