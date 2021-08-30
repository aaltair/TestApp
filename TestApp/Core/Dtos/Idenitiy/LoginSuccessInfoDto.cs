using System;
using System.Collections.Generic;

namespace TestApp.Core.Dtos.Idenitiy
{
    public class LoginSuccessInfoDto
    {
        public string UserId { set; get; }
        public string FullName { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Token { set; get; }
        public string ProfileImg { set; get; }
        public DateTime Expiration { set; get; }
        public ICollection<string> Roles { set; get; }
    }
}