using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Common.Domain.Shared;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Abstractions;
using UserManagement.Application.DTOs; 
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Models;
using UserManagement.Application.Features.Auth.Commands.Login; 
using UserManagement.Domain.Enums;
using UserManagement.Domain.Options;


namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types
{
     class ICloudLoginType : BaseExternalLogin
    {
        private readonly CustomUserManager _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly IAppleAuthService _appleAuthService;
        private readonly AppleSignInSettings _appleSettings;

        public ICloudLoginType(
            CustomUserManager userManager,
            IJwtProvider jwtProvider,
            IMapper mapper,
            IAppleAuthService appleAuthService,
            IOptions<AppleSignInSettings> appleSettingsOptions)
            : base(userManager, jwtProvider, mapper)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _appleAuthService = appleAuthService;
            _appleSettings = appleSettingsOptions.Value;
        }

        public override LoginProvider LoginProvider { get; set; } = LoginProvider.ICloud;

        public override async Task<ResponseModel<LoginCommandResponse>> Login(ExternalLoginCommand command)
        {
            if (command.ICloudLoginDto == null || string.IsNullOrEmpty(command.ICloudLoginDto.AuthorizationCode))
            {
                return ResponseModel.Failure<LoginCommandResponse>("Apple Sign In (ICloud) authorization code not provided.");
            }
            var iCloudLoginDetails = command.ICloudLoginDto;

            string clientSecret;
            try
            {
                clientSecret = _appleAuthService.GenerateAppleClientSecret();
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<LoginCommandResponse>($"Error generating Apple client secret: {ex.Message}");
            }

            AppleInternalTokenResponse? appleTokens;
            try
            {
                appleTokens = await _appleAuthService.ExchangeAuthCodeAsync(
                    iCloudLoginDetails.AuthorizationCode, 
                    clientSecret, 
                    _appleSettings.AppClientIdForAuthCode, 
                    CancellationToken.None);

                if (appleTokens == null || string.IsNullOrEmpty(appleTokens.IdToken))
                {
                    return ResponseModel.Failure<LoginCommandResponse>("Failed to exchange Apple authorization code or missing ID token.");
                }
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<LoginCommandResponse>($"Error during Apple token exchange: {ex.Message}");
            }

            ValidatedAppleIdTokenPayload? validatedIdTokenPayload;
            try
            {
                validatedIdTokenPayload = await _appleAuthService.ValidateAndDecodeAppleIdTokenAsync(
                    appleTokens.IdToken, 
                    _appleSettings.AppClientIdForAuthCode, 
                    CancellationToken.None);

                if (validatedIdTokenPayload == null)
                {
                    return ResponseModel.Failure<LoginCommandResponse>("Failed to validate Apple ID token.");
                }
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<LoginCommandResponse>($"Error during Apple ID token validation: {ex.Message}");
            }

            var appleUserId = validatedIdTokenPayload.Subject;
            var emailFromToken = validatedIdTokenPayload.Email;

            var loginInfo = new UserLoginInfo(LoginProvider.ToString(), appleUserId, "Apple");
            var user = await _userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);

            if (user == null)
            {
                string emailForNewUser = emailFromToken!;

                user = await _userManager.FindByEmailAsync(emailForNewUser);
                if (user != null)
                {
                    var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                    if (!addLoginResult.Succeeded)
                    {
                        return ResponseModel.Failure<LoginCommandResponse>(GetIdentityErrors(addLoginResult, "Failed to link Apple Sign In (ICloud) to existing user."));
                    }
                }
                else 
                {
                    // Assuming ApplicationUser is your IdentityUser-derived class
                    user = CreateCustomerUser(emailForNewUser,"", appleUserId);

                    // Using standard ASP.NET Identity user creation and login association
                    var createUserResult = await _userManager.CreateAsync(user);
                    if (!createUserResult.Succeeded)
                    {
                        return ResponseModel.Failure<LoginCommandResponse>(GetIdentityErrors(createUserResult));
                    }

                    var addLoginResultOnCreate = await _userManager.AddLoginAsync(user, loginInfo);
                    if (!addLoginResultOnCreate.Succeeded)
                    {
                        await _userManager.DeleteAsync(user); // Attempt to clean up the partially created user
                        return ResponseModel.Failure<LoginCommandResponse>(GetIdentityErrors(addLoginResultOnCreate, "Failed to add Apple (ICloud) login to newly created user."));
                    }
                }
            }

            var token = await _jwtProvider.Generate(user);

            return ResponseModel.Success(new LoginCommandResponse
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            });
        }

        private string GetIdentityErrors(IdentityResult result, string defaultError = "Identity operation failed.")
        {
            if (result.Errors.Any())
            {
                return string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            }
            return defaultError;
        }
    }
}