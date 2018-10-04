using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ePing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ePing.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;

        public LoginController(IConfiguration configuration,IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
           // HttpContext.Session.IsAvailable
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var endpoint = _configuration["IdentityUrl"] + _configuration["api:login_endpoint"];
            return View("Index",new LoginViewModel() { TokenEndPoint=endpoint});
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromQuery] string licenceOrName, [FromQuery] string prenom = null)
        {
            var endpoint =  _configuration["api:login_endpoint"];
            endpoint=  String.Format(endpoint, licenceOrName, prenom ?? "");

            
            HttpClient client = _factory.CreateClient("login");
            var response = await client.PostAsync(endpoint, new FormUrlEncodedContent(new Dictionary<string, string>()));
            string resultContent = await response.Content.ReadAsStringAsync();

            //return View("../Home/Index", resultContent);
            return Ok(resultContent);

        }
    }
}