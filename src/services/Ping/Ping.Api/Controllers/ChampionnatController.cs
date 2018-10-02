using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ping.Api.services;

namespace Ping.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionnatController : SpidControllerBase
    {
        public ChampionnatController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {

        }

        [HttpGet("{division}/{poule}/resultat")]
        public async Task<string> GetResultat(string division,string poule)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.RencontreResultat, new NameValueCollection() {
                {"auto","1" },
                {"D1", division},
                {"cx_poule",poule }
            });
        }

        [HttpGet("{division}/{poule}/classement")]
        public async Task<ActionResult<string>> GetClassement(string division, string poule)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.RencontreResultat, new NameValueCollection() {
                {"action","classement" },
                {"auto","1" },
                {"D1", division},
                {"cx_poule",poule }
            });
        }

        [HttpGet("{division}/{poule}/resultatdetail")]
        public async Task<string> GetResultatDetail(string division, string poule)
        {
            Task<string> t = GetResultat(division, poule);

            return await t;
        }
    }
}