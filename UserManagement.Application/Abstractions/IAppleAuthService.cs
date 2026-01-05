using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Models;

namespace UserManagement.Application.Abstractions
{
    public interface IAppleAuthService
    {
        Task<AppleInternalTokenResponse?> ExchangeAuthCodeAsync(string authorizationCode, string clientSecret, string appClientIdForTokenExchange, CancellationToken cancellationToken);
        Task<ValidatedAppleIdTokenPayload?> ValidateAndDecodeAppleIdTokenAsync(string idToken, string expectedAudience, CancellationToken cancellationToken);
        string GenerateAppleClientSecret();
    }
}
