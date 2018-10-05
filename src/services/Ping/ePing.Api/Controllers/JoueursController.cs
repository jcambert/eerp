using ePing.Api.dbcontext;
using ePing.Api.models;
using ePing.Api.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueursController : PingControllerBase
    {


        public JoueursController(PingContext context, IJoueurService service) : base(context)
        {
            Service = service;
        }

        public IJoueurService Service { get; }

        // GET: api/Joueurs
        [HttpGet]
        public IEnumerable<Joueur> GetJoueur()
        {
            return Context.Joueur;
        }

        [HttpGet("club/load/{numero}")]
        [HttpGet("club/{numero}")]
        public async Task<IActionResult> GetJoueursDuClub([FromRoute] string numero)
        {
            var club = await Context.Clubs.FindAsync(numero);
            if (club == null)
                return NotFound();

            if (Request.Path.Value.Contains("load"))
            {
                await Service.loadListForClubFromSpid(numero, true, club);

            }


            return Ok(Context.Joueur.Where(j => j.Club.idClub == club.idClub));
        }

        // GET: api/Joueurs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJoueur([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var joueur = await Context.Joueur.FindAsync(id);

            if (joueur == null)
            {
                return NotFound();
            }

            return Ok(joueur);
        }

        // PUT: api/Joueurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoueur([FromRoute] string id, [FromBody] Joueur joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != joueur.Licence)
            {
                return BadRequest();
            }

            Context.Entry(joueur).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoueurExists(id))
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

        // POST: api/Joueurs
        [HttpPost]
        public async Task<IActionResult> PostJoueur([FromBody] Joueur joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Joueur.Add(joueur);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetJoueur", new { id = joueur.Licence }, joueur);
        }

        // DELETE: api/Joueurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoueur([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var joueur = await Context.Joueur.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }

            Context.Joueur.Remove(joueur);
            await Context.SaveChangesAsync();

            return Ok(joueur);
        }

        private bool JoueurExists(string id)
        {
            return Context.Joueur.Any(e => e.Licence == id);
        }
    }
}