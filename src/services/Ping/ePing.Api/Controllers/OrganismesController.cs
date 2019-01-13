using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ePing.Api.dbcontext;
using ePing.Api.models;
using ePing.Api.services;

namespace ePing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganismesController : PingControllerBase
    {
        

        public OrganismesController(/*PingDbContext context,*/IOrganismeService service):base(/*context*/)
        {
            this.Service = service;
        }

        public IOrganismeService Service { get;  }


        


        // GET: api/Organismes
        [HttpGet]
        [HttpGet("force")]
        public async Task<IEnumerable<Organisme>> GetOrganisme()
        {
            await Service.Load();// LoadFromSpid(Request.Path.Value.Contains("force"));

            // return Context.Organismes;
            return null;
        }

        // GET: api/Organismes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganisme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Service.Load();// LoadFromSpid();
            /*var organisme = await Context.Organismes.FindAsync(id);
            
            if (organisme == null)
            {
                return NotFound();
            }

            return Ok(organisme);*/
            return Ok();
        }

        // PUT: api/Organismes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganisme([FromRoute] string id, [FromBody] Organisme organisme)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organisme.Id)
            {
                return BadRequest();
            }

            Context.Entry(organisme).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganismeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
            return Ok();
        }

        // POST: api/Organismes
        [HttpPost]
        public async Task<IActionResult> PostOrganisme([FromBody] Organisme organisme)
        {
            /*  if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              Context.Organismes.Add(organisme);
              await Context.SaveChangesAsync();

              return CreatedAtAction("GetOrganisme", new { id = organisme.Id }, organisme);*/
            return Ok();
        }

        // DELETE: api/Organismes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisme([FromRoute] int id)
        {
            /* if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             var organisme = await Context.Organismes.FindAsync(id);
             if (organisme == null)
             {
                 return NotFound();
             }

             Context.Organismes.Remove(organisme);
             await Context.SaveChangesAsync();

             return Ok(organisme);*/
            return Ok();
        }

        private bool OrganismeExists(string id)
        {
            return false;
           // return Context.Organismes.Any(e => e.Id == id);
        }
    }
}