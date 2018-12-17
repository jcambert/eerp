using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LogoutController : Controller
    {
        public LogoutController(SignInManager<IdentityUser> signInManager,)
        {

        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}