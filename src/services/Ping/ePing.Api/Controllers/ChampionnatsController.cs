using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePing.Api.dbcontext;
using ePing.Api.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionnatsController : PingControllerBase
    {
        public ChampionnatsController(PingDbContext context,IChampionnatService service) : base(context)
        {
            Service = service;
        }

        public IChampionnatService Service { get; }

        [HttpGet("{division}/{poule}/resultat")]
        public async Task<IActionResult> ResultatEquipe([FromRoute] string division,[FromRoute] string poule)
        {
            var resultats= await Service.GetResultats(division, poule);
            return Ok(resultats);
        }

        [HttpGet("{division}/{poule}/classement")]
        public async Task<IActionResult> ClassementEquipe([FromRoute] string division, [FromRoute] string poule)
        {
            var classements = await Service.GetClassements(division, poule);
            return Ok(classements);
        }
    }
}