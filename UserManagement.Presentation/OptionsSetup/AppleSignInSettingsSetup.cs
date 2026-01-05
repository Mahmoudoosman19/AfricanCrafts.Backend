using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserManagement.Domain.Options;

namespace UserManagement.Presentation.OptionsSetup
{
    public class AppleSignInSettingsSetup : IConfigureOptions<AppleSignInSettings>
    {
        private const string SectionName = "AppleSignIn";
        private readonly IConfiguration _configuration;

        public AppleSignInSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AppleSignInSettings options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
