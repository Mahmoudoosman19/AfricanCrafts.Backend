using System;
using System.Text.Json.Serialization;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Models
{
    public class AppleInternalTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = null!;
        
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; } = null!;
        
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = null!;
    }

    public class ValidatedAppleIdTokenPayload
    {
        public string Subject { get; set; } = null!; // User's unique Apple ID
        public string? Email { get; set; }
        public string Issuer { get; set; } = null!;
        public string? Audience { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime IssuedAt { get; set; }
    }
} 