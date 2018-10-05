using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ePing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private ApiSettings _settings;

        public DashboardController(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet("settings")]
        public ActionResult<string> GetSettings()
        {
            string auth = HttpContext.Session.GetValue<string>("auth");
            var user = JsonConvert.DeserializeObject<UserDto>(auth);

            var token = JsonConvert.DeserializeObject<BearerDto>(auth);

            DashboardViewModel vm = new DashboardViewModel() { User = user.User, Token = token.Jwt, ApiSettings = _settings };

            return Ok(vm.ToJson());
        }
    }
}