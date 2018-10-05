using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ePing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private ApiSettings _settings;

        public SettingsController(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet]
        public ActionResult<string> GetSettings()
        {
            return Ok(_settings.ToJson());
        }
    }
}