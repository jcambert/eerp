using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ping.Api.services;

namespace Ping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : SpidControllerBase { 


        public JoueurController(ISpidRequest request,ISpidConfiguration configuration):base(request,configuration)
        {

        }

        [HttpGet("club/{numero}")]
        public async Task<ActionResult<string>> GetByClub(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.LicenceJoueur, new NameValueCollection() {
                {"club", numero }
            });
        }
        
        
    }
    
}