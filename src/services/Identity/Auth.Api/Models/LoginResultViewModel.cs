using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Models
{
    public class LoginResultViewModel
    {
        public string Jwt { get; set; }
        public PingUser User { get; set; }
        public string ReturnUrl { get; set; }
    }
}
