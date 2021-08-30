using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Core.Dtos.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryDay { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
