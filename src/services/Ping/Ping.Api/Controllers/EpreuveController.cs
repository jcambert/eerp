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
    public class EpreuveController : SpidControllerBase
    {
        public EpreuveController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {

        }

        [HttpGet("{organisme}/{type}")]
        public async Task<ActionResult<string>> GetByOrganisme(string organisme,string @type)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Epreuve, new NameValueCollection() {
                {"organisme", organisme },
                {"type",@type }
            });
        }

        [HttpGet("{organisme}/{type}/{epreuve}")]
        public async Task<ActionResult<string>> GetDivisionParEpreuve(string organisme,string epreuve, string @type)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.DivisionEpreuve, new NameValueCollection() {
                {"organisme", organisme },
                {"epreuve",epreuve },
                {"type",@type }
            });
        }
    }
}