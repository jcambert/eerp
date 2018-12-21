using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using ePing.Api.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueursController : PingControllerBase
    {


        public JoueursController(PingDbContext context, IJoueurService service, PointService pointsService, IClubService clubService) : base(context)
        {
            Service = service;
            PointsService = pointsService;
            ClubService = clubService;
        }

        public IJoueurService Service { get; }

        public PointService PointsService { get; }
        public IClubService ClubService { get; }

        // GET: api/Joueurs
        [HttpGet]
        public IEnumerable<Joueur> GetJoueurs()
        {
            return Context.Joueurs;
        }

      /*  [HttpGet("{licence}")]
        public async Task<IActionResult> GetJoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
            return Ok(joueur);
        }*/

        // GET: api/Joueurs
        [HttpGet("{licence}/parties")]
        public async Task<IActionResult> GetPartiesDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);// Service.LoadDetailJoueur(licence);
            var parties = await Service.loadJoueurParties(joueur);

            var journees = new List<Journee>();
            var toto = parties/*.OrderBy(d => d.Date)*/.GroupBy(x => x.Date).ToList();
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

            return Ok(journees.OrderByDescending(j=>j.Date));
        }
        [HttpGet("{licence}/historique")]
        public async Task<IActionResult> GetHistoriquesDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
            var parties = await Service.loadJoueurHistoriques(joueur);
            var journee = new JourneeHistoriques();
            journee.AddRange(parties);
            return Ok(journee);
        }

        [HttpGet("{licence}/histoclass")]
        public async Task<IActionResult> GetHistoriqueClassementDujoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
            var histo = await Service.loadHistoriqueClassement(joueur);

            histo.AddClassementActuel(joueur);

            return Ok(histo);
        }
        [HttpGet("{licence}/histopoint")]
        public async Task<IActionResult> GetHistoriquePointDujoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
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

            var histo = Service.ConvertToHistoriquePoint(journees).OrderByDescending(h => h.Date).ToArray();
            for (int i = 1; i < histo.Count(); i++)
            {
                histo[i].PointsGagnesPerdus += histo[i - 1].PointsGagnesPerdus;
            }


            return Ok(histo);
        }

        [HttpGet("{licence}/histovictoire")]
        public async Task<IActionResult> GetHistoriqueVictoireDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
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

            var histo = Service.ConvertToHistoriqueVictoire(journees).OrderBy(h => h.Date).ToArray();

            for (int i = 1; i < histo.Count(); i++)
            {
                histo[i].Victoire += histo[i - 1].Victoire;
            }
            return Ok(histo);
        }

        [HttpGet("{licence}/histodefaite")]
        public async Task<IActionResult> GetHistoriquedefaiteDuJoueur([FromRoute] string licence)
        {
            var joueur = await Service.LoadJoueur(licence);
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

            var histo = Service.ConvertToHistoriqueDefaite(journees).OrderBy(h => h.Date).ToArray();

            for (int i = 1; i < histo.Count(); i++)
            {
                histo[i].Defaite += histo[i - 1].Defaite;
            }
            return Ok(histo);
        }


        //[HttpGet("club/{numero}/load")]
        [HttpGet("club/{numero}")]
        public async Task<IActionResult> GetJoueursDuClub([FromRoute] string numero)
        {
            var club = await ClubService.LoadClub(numero);// Context.Clubs.FindAsync(numero);
            if (club == null)
                return NotFound();
            var liste = await Service.LoadJoueurs(club);
           /* if (Request.Path.Value.Contains("load"))
            {

                foreach (var joueur in liste)
                {
                    await Service.LoadDetailJoueur(joueur.Licence, true, club);
                }

            }*/


           // var result = Context.Joueurs.Where(j => j.Club == club).Include("Extra");

            //var result1 = await result.ToListAsync();

            return Ok(liste);
        }

        // GET: api/Joueurs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJoueur([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var joueur = await Service.LoadJoueur(id);// Context.Joueurs.FindAsync(id);

            if (joueur == null)
            {
                return NotFound();
            }

            return Ok(joueur);
        }

        [HttpPut("{licence}/extra")]
        public async Task<IActionResult> PutExtra([FromRoute]string licence, [FromBody]JoueurExtraDto extra)
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
        public async Task<IActionResult> PutJoueur([FromRoute] string licence, [FromBody] Joueur joueur)
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
        public async Task<IActionResult> PostJoueur([FromBody] Joueur joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Joueurs.Add(joueur);
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

            var joueur = await Context.Joueurs.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }

            Context.Joueurs.Remove(joueur);
            await Context.SaveChangesAsync();

            return Ok(joueur);
        }

        private bool JoueurExists(string id)
        {
            return Context.Joueurs.Any(e => e.Licence == id);
        }
    }
}