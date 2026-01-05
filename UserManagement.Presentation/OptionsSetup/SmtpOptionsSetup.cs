using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserManagement.Domain.Options;

namespace UserManagement.Presentation.OptionsSetup
{
    public class SmtpOptionsSetup : IConfigureOptions<SmtpOptions>
    {
        private const string SectionName = "SmtpOptions";

        private readonly IConfiguration _configuration;

        public SmtpOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(SmtpOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
