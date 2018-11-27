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
    public class ParametresController : ControllerBase
    {
        public ParametreService Service { get; }
        public IntranetDbContext Context => Service.Context;
        public IRepository<IntranetDbContext, Parametre> Repository => Service.Repository;

        public ParametresController(ParametreService parametreService)
        {
            Service = parametreService;
        }

        // GET: api/Parametres
        [HttpGet]
        public IEnumerable<Parametre> GetParametres()
        {
            return Repository.Get();
        }

        // GET: api/Parametres
        [HttpGet("flat")]
        public IActionResult GetFlatParametres()
        {
            var result = Service.Flatten();
            return Ok(result);
        }
        // GET: api/Parametres/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetParametre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametre = await Repository.GetByIdAsync(id);// (  Context.Parametres.FindAsync(id);

            if (parametre == null)
            {
                return NotFound();
            }

            return Ok(parametre);
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

        // PUT: api/Parametres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametre([FromRoute] int id, [FromBody] Parametre parametre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametre.Id)
            {
                return BadRequest();
            }

            Repository.Update(parametre);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Parametres
        [HttpPost]
        public async Task<IActionResult> PostParametre([FromBody] Parametre parametre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Repository.Insert(parametre);

            await Context.SaveChangesAsync();

            return CreatedAtAction("GetParametre", new { id = parametre.Id }, parametre);
        }

        // DELETE: api/Parametres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var parametre=await Repository.DeleteAsync(id);
            
            if (parametre == null)
            {
                return NotFound();
            }
            
            await Context.SaveChangesAsync();

            return Ok(parametre);
        }

        private bool ParametreExists(int id)
        {
            return Repository.Exists(id);
        }
    }
}