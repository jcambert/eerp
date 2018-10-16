using ePing.Api.dbcontext;
using ePing.Api.dto;
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


        public JoueursController(PingDbContext context, IJoueurService service, PointService pointsService) : base(context)
        {
            Service = service;
            PointsService = pointsService;
        }

        public IJoueurService Service { get; }

        public PointService PointsService { get; }

        // GET: api/Joueurs
        [HttpGet]
        public IEnumerable<JoueurSpid> GetJoueur()
        {
            return Context.JoueurSpid;
        }

        // GET: api/Joueurs
        [HttpGet("{licence}/parties")]
        public async Task<IActionResult> GetPartiesDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.loadDetailJoueur(licence);
            var parties = await Service.loadJoueurParties(joueur);
            var journees = new List<Journee>();
            var toto = parties.GroupBy(x => x.Date).ToList();
            toto.ForEach(titi =>
            {
                var journee = new Journee() { Date = titi.Key };
                titi.ToList().ForEach(partie =>
                {
                    journee.Epreuve = partie.Epreuve;
                    journee.Add(joueur, partie, PointsService);
                });
                journees.Add(journee);
            });

            return Ok(journees);
        }
        [HttpGet("{licence}/historique")]
        public async Task<IActionResult> GetHistoriquesDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.loadDetailJoueur(licence);
            var parties = await Service.loadJoueurHistoriques(joueur);
            var journee = new JourneeHistoriques();
            journee.AddRange(parties);
            return Ok(journee);
        }

        [HttpGet("{licence}/histoclass")]
        public async Task<IActionResult>GetHistoriqueClassementDujoueur([FromRoute] string licence)
        {
            var joueur = await Service.loadDetailJoueur(licence);
            var histo = await Service.loadHistoriqueClassement(joueur);

            histo.AddClassementActuel(joueur);

            return Ok(histo);
        }

        [HttpGet("club/{numero}/load")]
        [HttpGet("club/{numero}")]
        public async Task<IActionResult> GetJoueursDuClub([FromRoute] string numero)
        {
            var club = await Context.Clubs.FindAsync(numero);
            if (club == null)
                return NotFound();

            if (Request.Path.Value.Contains("load"))
            {
                var liste = await Service.loadListForClubFromSpid(numero, true, club);
                foreach (var joueur in liste)
                {
                    await Service.loadDetailJoueur(joueur.Licence, true, club);
                }

            }


            var result = Context.JoueurSpid.Where(j => j.Club == club).Include("Extra");

            var result1 = await result.ToListAsync();

            return Ok(result1);
        }

        // GET: api/Joueurs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJoueur([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var joueur = await Context.JoueurSpid.FindAsync(id);

            if (joueur == null)
            {
                return NotFound();
            }

            return Ok(joueur);
        }

        [HttpPut("{licence}/extra")]
        public async Task<IActionResult> PutExtra([FromRoute]string licence, [FromBody]JoueurDto extra)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (licence != extra.Licence)
            {
                return BadRequest();
            }

            Context.Entry(extra).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Joueurs/5
        [HttpPut("{licence}")]
        public async Task<IActionResult> PutJoueur([FromRoute] string licence, [FromBody] JoueurSpid joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (licence != joueur.Licence)
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
                if (!JoueurExists(licence))
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
        public async Task<IActionResult> PostJoueur([FromBody] JoueurSpid joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.JoueurSpid.Add(joueur);
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

            var joueur = await Context.JoueurSpid.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }

            Context.JoueurSpid.Remove(joueur);
            await Context.SaveChangesAsync();

            return Ok(joueur);
        }

        private bool JoueurExists(string id)
        {
            return Context.JoueurSpid.Any(e => e.Licence == id);
        }
    }
}