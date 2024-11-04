using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGL.Auth.Domain.Settings
{
    public class OneLoginSettings
    {
        public required string Endpoint { get; set; }
        public required string AppId { get; set; }
        public required string SecretKey { get; set; }
        public required string EncryptedCreds { get; set; }
        public required string GrantType { get; set; }
        public required string SubDomainName { get; set; }

    }
}
