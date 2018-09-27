using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public class SpidSignInResult
    {
        public SpidSignInResult()
        {

        }

        public SignInResult SignInResult { get; set; }

        public PingUser User{ get; set; }
    }
}
