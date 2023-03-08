using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Options
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutesToken { get; set; }
        public int ExpireMinutesRefreshToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
