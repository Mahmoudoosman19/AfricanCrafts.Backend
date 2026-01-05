using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Options
{
    public class AppleSignInSettings
    {
        public string TeamId { get; set; } = null!;
        public string BackendServiceId { get; set; } = null!;
        public string KeyId { get; set; } = null!;
        public string PrivateKeyP8Content { get; set; } = null!;
        public string RedirectUri { get; set; } = null!;
        public string AppClientIdForAuthCode { get; set; } = null!;
    }
}
