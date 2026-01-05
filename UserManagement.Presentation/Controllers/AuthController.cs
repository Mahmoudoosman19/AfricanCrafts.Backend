using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Auth.Commands.ChangePassword;
using UserManagement.Application.Features.Auth.Commands.ChangePhoneNumber;
using UserManagement.Application.Features.Auth.Commands.ChangeUserEmail;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin;
using UserManagement.Application.Features.Auth.Commands.ExternalLoginExtraDetails;
using UserManagement.Application.Features.Auth.Commands.ForgotPassword;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Application.Features.Auth.Commands.Register;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Features.Auth.DashboardRole;
using UserManagement.Application.Features.User.Commands.UpdateUserProfile;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]
    public sealed class AuthController : ApiController
    {

        public AuthController(ISender sender) : base(sender)
        {
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginCustomer([FromQuery] LoginCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] CustomerRegisterDto command)
        {
            var registerCustomer = new RegisterCommand(command, RegisterType.Customer);
            var result = await Sender.Send(registerCustomer);
            return HandleResult(result);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginCommand command)
        {
            var result = await Sender.Send(command);

            return HandleResult(result);
        }

        [HttpPost("external-login-extra-details")]
        public async Task<IActionResult> ExternalLoginExtraDetails([FromBody] ExternalLoginExtraDetailsCommand command)
        {
            var result = await Sender.Send(command);

            return HandleResult(result);
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand updatedProfile, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(updatedProfile);
            return HandleResult(response);
        }
        [HttpGet("Get-Dashboard-Role")]
        public async Task<IActionResult> GetDashboardRole([FromQuery] DashboardRoleQuery dashboardRole, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(dashboardRole);
            return HandleResult(response);
        }
        [HttpPut("Change-User-Email")]
        public async Task<IActionResult> ChangeUserEmail([FromBody] ChangeUserEmailCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        [HttpPut("Change-User-PhoneNumber")]
        public async Task<IActionResult> ChangeUserPhoneNumber([FromBody] ChangePhoneNumberCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
    }
}