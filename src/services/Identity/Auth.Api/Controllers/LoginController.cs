using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Api.Models;
using Auth.Api.Repositories;
using Auth.Api.Services;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {

       // private readonly SpidUserManager _userManager;
        //private readonly SpidSignInManager _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IResourceOwnerPasswordValidator _pv;
        private readonly AuthRequest _authRequest;
        private readonly JoueurRepository _repo;

        public LoginController(/*SpidUserManager userManager,
                                SpidSignInManager signInManager,*/
                                AuthRequest authRequest,
                                JoueurRepository repo,
                                IConfiguration configuration,
                                ILoggerFactory loggerFactory,
                                IResourceOwnerPasswordValidator pv)
        {
          //  _userManager = userManager;
          //  _signInManager = signInManager;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _pv = pv;
            _authRequest = authRequest;
            _repo = repo;

        }

       /* [HttpPost("i4")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResult>> LoginWithIdentityServer4([FromQuery] string licence, [FromQuery]bool rememberMe = false, [FromQuery]string returnUrl = null)
        {
            
            HttpContext.Authentication.SignInAsync(licence,)
            return new LoginResult() { };
        }*/

        // POST: /Login/
        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult<LoginResult>> Index([FromQuery] string licenceOrName,[FromQuery] string prenom =null)
        {
            licenceOrName =  licenceOrName?.ToUpper();
            prenom = prenom.TitleCase();

            var token = await _authRequest.Login(Request.BaseUrl(), licenceOrName??"",prenom);
            if (token.IsError)
                return Unauthorized();

            int licence;
            PingUser user;
            if (int.TryParse(licenceOrName, out licence))
                user = await _repo.FindByLicenceAsync(licenceOrName);
            else
                user = await _repo.FindByNameAsync(licenceOrName, prenom);
            return new LoginResult() { Jwt = token.AccessToken, User = user ?? default(PingUser) };
                

        }
    }
}