using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Api.Models;
using Auth.Api.Services;
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

        private readonly SpidUserManager _userManager;
        private readonly SpidSignInManager _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        public LoginController(SpidUserManager userManager,
                                SpidSignInManager signInManager,
                                IConfiguration configuration,
                                ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<AccountController>();

        }
        //
        // POST: /Login/
        [HttpPost]

        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult<LoginResult>> Index([FromQuery] string licence, [FromQuery]bool rememberMe = false, [FromQuery]string returnUrl = null)
        {
            

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(model.Input.Licence, model.Password, model.RememberMe, lockoutOnFailure: false);
                var result = await _signInManager.LicenceSignInAsync(licence, rememberMe);
                if (result.SignInResult.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    //return RedirectToLocal(returnUrl);
                    return new LoginResult() { Jwt = await GenerateJwtToken(result.User), User = result.User, ReturnUrl= returnUrl };
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest();
                }

        }

        private async Task<string> GenerateJwtToken(PingUser user)
        {
            var t = Task.Run(() => {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email??"tmp@tmp.fr"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["auth:barear:JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["auth:barear:JwtExpireDays"]));

                var token = new JwtSecurityToken(
                    _configuration["auth:barear:JwtIssuer"],
                    _configuration["auth:barear:JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
            return await t;
        }
    }
}