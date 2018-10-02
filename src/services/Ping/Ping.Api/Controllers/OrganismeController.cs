using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ping.Api.services;

namespace Ping.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganismeController : SpidControllerBase
    {
        public OrganismeController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {

        }
        [HttpGet("{type}")]
        public async Task<ActionResult<string>> GetByType(string @type)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Organisme, new NameValueCollection() {
                {"type", @type}
            });
        }
    }
}