
using Auth.Api.Data;
using Auth.Api.Models;
using Auth.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly HttpClientRequest _clientRequest;
        private readonly IConfiguration _configuration;

        public AccountController(HttpClientRequest clientRequest,IConfiguration configuration)
        {
            _clientRequest = clientRequest;
            _configuration = configuration;
        }
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // GET: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] string licence, [FromQuery]bool rememberMe = false, [FromQuery]string returnUrl = null)
        {
            var uri = Request.PathBase + "/Login/";
            NameValueCollection parameters = new NameValueCollection();
            parameters.Set("licence", licence);
            parameters.Set("rememberMe", rememberMe.ToString());
            parameters.Set("returnUrl", returnUrl);

            var result = await _clientRequest.ExecuteGet< LoginResultViewModel,LoginResult>(_configuration.GetValue<string>("auth:name"), uri, parameters);

            if (returnUrl == null)
                return RedirectToLocal(returnUrl);
            return View(result);
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        
    }
}