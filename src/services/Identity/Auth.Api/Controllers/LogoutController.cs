using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Auth.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly AuthRequest _authRequest;

        public LogoutController(AuthRequest authRequest)
        {
            _authRequest = authRequest;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromForm]string token)
        {
            token = Regex.Replace(token, "bearer", "", RegexOptions.IgnoreCase).Trim();
            var resp = await _authRequest.Logout(Request.BaseUrl(),token);
          
            return Ok(resp);
        }
    }

    public class TokenModel
    {
        public string token { get; set; }
    }
}