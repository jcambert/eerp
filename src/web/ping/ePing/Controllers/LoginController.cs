using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ePing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ePing.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
        private readonly ApiSettings _settings;
        public LoginController(IConfiguration configuration,IHttpClientFactory factory, IOptions<ApiSettings> settings)
        {
            _configuration = configuration;
            _factory = factory;
            this._settings = settings.Value;
           // HttpContext.Session.IsAvailable
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var endpoint = _configuration["ping:auth:IdentityUrl"] + _configuration["ping:auth:login_endpoint"];
            return View("Index",new LoginViewModel() { TokenEndPoint=endpoint});
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromBody] UserLoginData user)
        {
            try
            {
                var endpoint = _configuration["ping:auth:login_endpoint"];
                endpoint = String.Format(endpoint, user.licenceOrName, user.prenom ?? "");
                endpoint = "/Login";
                FormUrlEncodedContent dataForm = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string,string>("licenceOrName",user.licenceOrName),
                    new KeyValuePair<string, string>("prenom",user.prenom)
                });
                HttpClient client = _factory.CreateClient("auth");
                var response = await client.PostAsync(endpoint, dataForm);
                
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return Forbid();
                }
                else
                {
                    string resultContent = await response.Content.ReadAsStringAsync();
                    HttpContext.Session.SetValue("auth", resultContent);
                    return Ok(CreateVM().ToJson());
                }
                
            }
            catch 
            {
                return this.NotFound();
            }

        }

        DashboardViewModel CreateVM(string viewingClub = "")
        {
            string auth = HttpContext.Session.GetValue<string>("auth");
            var user = JsonConvert.DeserializeObject<UserDto>(auth);

            var token = JsonConvert.DeserializeObject<BearerDto>(auth);


            DashboardViewModel vm = new DashboardViewModel() { User = user.User, Token = token.Jwt, ViewingClub = viewingClub,ApiSettings=_settings };
            return vm;
        }
    }

    public class UserLoginData
    {
        public string licenceOrName;
        public string prenom;
    }
}