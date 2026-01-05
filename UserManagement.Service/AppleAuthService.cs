using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Models;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types;
using UserManagement.Domain.Options;

namespace UserManagement.Persistence
{
    public class AppleAuthService : IAppleAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppleSignInSettings _appleSettings;

        public AppleAuthService(
            IHttpClientFactory httpClientFactory,
            IOptions<AppleSignInSettings> appleSettingsOptions)
        {
            _httpClientFactory = httpClientFactory;
            _appleSettings = appleSettingsOptions.Value;

            if (string.IsNullOrEmpty(_appleSettings.RedirectUri) ||
                string.IsNullOrEmpty(_appleSettings.BackendServiceId) ||
                string.IsNullOrEmpty(_appleSettings.AppClientIdForAuthCode) ||
                string.IsNullOrEmpty(_appleSettings.TeamId) ||
                string.IsNullOrEmpty(_appleSettings.KeyId) ||
                string.IsNullOrEmpty(_appleSettings.PrivateKeyP8Content))
            {
                throw new InvalidOperationException("AppleSignInSettings: One or more required settings are missing. Ensure TeamId, BackendServiceId, AppClientIdForAuthCode, KeyId, PrivateKeyP8Content, and RedirectUri are configured.");
            }
        }

        public string GenerateAppleClientSecret()
        {
            var now = DateTime.UtcNow;
            using var ecdsa = ECDsa.Create();
            try
            {
                byte[] keyBytes = Convert.FromBase64String(_appleSettings.PrivateKeyP8Content);
                ecdsa.ImportPkcs8PrivateKey(keyBytes, out _);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to import Apple private key. Ensure PrivateKeyP8Content in AppleSignInSettings is a valid Base64 encoded PKCS#8 ECDSA key. Details: {ex.Message}", ex);
            }

            var securityKey = new ECDsaSecurityKey(ecdsa) { KeyId = _appleSettings.KeyId };
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _appleSettings.TeamId,
                Audience = "https://appleid.apple.com",
                Subject = new ClaimsIdentity(new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, _appleSettings.BackendServiceId) }),
                IssuedAt = now,
                Expires = now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.CreateJwtSecurityToken(descriptor);
            if (string.IsNullOrEmpty(jwtToken.Header.Kid))
            {
                jwtToken.Header.Add("kid", _appleSettings.KeyId);
            }
            return handler.WriteToken(jwtToken);
        }

        public async Task<AppleInternalTokenResponse?> ExchangeAuthCodeAsync(string authorizationCode, string clientSecret, string appClientIdForTokenExchange, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("AppleAuthClient");
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("YourApplicationName/1.0 (Backend)");

            var tokenRequestParams = new Dictionary<string, string>
            {
                { "client_id", appClientIdForTokenExchange },
                { "client_secret", clientSecret },
                { "code", authorizationCode },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _appleSettings.RedirectUri }
            };

            var requestContent = new FormUrlEncodedContent(tokenRequestParams);
            var response = await client.PostAsync("https://appleid.apple.com/auth/token", requestContent, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Apple token endpoint request failed. Status: {response.StatusCode}. Response: {responseContent}");
            }
            return JsonSerializer.Deserialize<AppleInternalTokenResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ValidatedAppleIdTokenPayload?> ValidateAndDecodeAppleIdTokenAsync(string idToken, string expectedAudience, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var applePublicKeys = await GetApplePublicKeysAsync(cancellationToken);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = applePublicKeys,
                ValidateIssuer = true,
                ValidIssuer = "https://appleid.apple.com",
                ValidateAudience = true,
                ValidAudience = expectedAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };

            ClaimsPrincipal principal;
            JwtSecurityToken jwtValidatedToken;
            principal = tokenHandler.ValidateToken(idToken, validationParameters, out SecurityToken validatedToken);
            jwtValidatedToken = (JwtSecurityToken)validatedToken;

            var subject = principal.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? jwtValidatedToken.Subject;
            if (string.IsNullOrEmpty(subject))
            {
                throw new SecurityTokenValidationException("Apple ID token is missing the required 'sub' (subject) claim.");
            }

            var email = principal.FindFirstValue(JwtRegisteredClaimNames.Email) ?? jwtValidatedToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? principal.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return new ValidatedAppleIdTokenPayload
            {
                Subject = subject,
                Email = email,
                Issuer = jwtValidatedToken.Issuer,
                Audience = jwtValidatedToken.Audiences.FirstOrDefault(),
                ExpiresAt = jwtValidatedToken.ValidTo,
                IssuedAt = jwtValidatedToken.ValidFrom
            };
        }

        private async Task<IEnumerable<SecurityKey>> GetApplePublicKeysAsync(CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("AppleKeysClient");
            var response = await client.GetAsync("https://appleid.apple.com/auth/keys", cancellationToken);
            response.EnsureSuccessStatusCode();
            var keysJson = await response.Content.ReadAsStringAsync(cancellationToken);
            var jwks = new JsonWebKeySet(keysJson);
            return jwks.Keys;
        }
    }
}