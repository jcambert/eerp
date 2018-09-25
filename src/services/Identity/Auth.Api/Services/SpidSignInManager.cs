using Auth.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public class SpidSignInManager : SignInManager<PingUser>
    {
        public SpidSignInManager(UserManager<PingUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<PingUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<PingUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }



        public new SpidUserManager UserManager => base.UserManager as SpidUserManager;

        internal async Task<SignInResult> LicenceSignInAsync(string licence, bool rememberMe)
        {
            
            var result = await UserManager.FindByLicenceAsync(licence);

            if (result != null)
                return SignInResult.Success;
            return SignInResult.Failed;
        }
    }
}
