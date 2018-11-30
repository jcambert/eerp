using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;
using Intranet.Api.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametresController : ApiServiceBaseController<ParametreService,Parametre>
    {


        public ParametresController(ParametreService service):base(service)
        {

        }


        // GET: api/Parametres
        [HttpGet("flat")]
        public IActionResult GetFlatParametres()
        {
            var result = Service.Flatten();
            return Ok(result);
        }
        
        // GET: api/Parametres/TypeMatiere
        [HttpGet("{type:alpha}")]
        public IActionResult GetParametreByType([FromRoute] string type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var parametre = Service.GetByType(type);

            if (parametre == null)
            {
                return NotFound();
            }

            return Ok(parametre);
        }
        // GET: api/Parametres/CodePrimaire/CodeSecondaire
        [HttpGet("code/{primaire}/{Secondaire?}")]
        public IActionResult GetParametreByCodeprimaire([FromRoute] int primaire, int? secondaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametre = Service.GetByCode(primaire, secondaire);

            if (parametre == null)
            {
                return NotFound();
            }

            return Ok(parametre);
        }
        
    }
}