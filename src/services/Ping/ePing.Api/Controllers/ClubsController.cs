using ePing.Api.dbcontext;
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
    public class ClubsController : PingControllerBase
    {



        public ClubsController(PingDbContext context, IClubService service, IOrganismeService organismeService, IChampionnatService championnatService, QueryService queryService/*, CacheService cache*/) : base(context)
        {

            Service = service;
            OrganismeService = organismeService;
            ChampionnatService = championnatService;
            QueryService = queryService;
          //  CacheService = cache;
        }

        public IClubService Service { get; }
        public IOrganismeService OrganismeService { get; }
        public IChampionnatService ChampionnatService { get; }
        public QueryService QueryService { get; }
        //public CacheService CacheService { get; }
        // GET: api/Clubs
        [HttpGet]
        public IEnumerable<Club> GetClubs()
        {
            return Context.Clubs;
        }

        // GET: api/Clubs/5
        [HttpGet("{numero}/load")]
        [HttpGet("{numero}")]
        public async Task<IActionResult> GetClub([FromRoute] string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var club = await Service.LoadClub(numero);

            if (club == null)
            {

                if (Request.Path.Value.Contains("/load"))
                {
                    club = await Service.LoadClubFromSpid(numero, true);
                }
                if (club == null) return NotFound();
            }

            return Ok(club);
        }


        [HttpGet("{numero}/equipes/")]
        [HttpGet("{numero}/equipes/resultats")]
        public async Task<IActionResult> GetEquipesDuclub([FromRoute]string numero)
        {
            var club = await Service.LoadClub(numero);
            if (club == null) return NotFound();
            var full = Request.Path.Value.Contains("resultats");
            //var organismes = await OrganismeService.Load();
            var equipes = await Service.LoadEquipes(club, "M");
            if (full)
            {
               /* Func<Equipe, Task> getClassementsAsync = (equipe) =>
               {
                   return ChampionnatService.LoadClassements(equipe);
                 
               };
                Func<Equipe, Task> getResultatsAsync = (equipe) =>
                {
                    return ChampionnatService.LoadResultats(equipe);
                };*/
                //var tasks = new List<Task>();
                foreach (var equipe in equipes)
                {
                    //var pere = QueryService.Parse(equipe.LienDivision).Where(q => q.Key == "organisme_pere").FirstOrDefault();

                    //equipe.Organisme = organismes.Where(o => o.Identifiant == pere.Value).FirstOrDefault();
                    await ChampionnatService.LoadClassements(equipe);
                    await ChampionnatService.LoadResultats(equipe);
                    //tasks.Add(getClassementsAsync(equipe));
                    //tasks.Add(getResultatsAsync(equipe));
                }
               // await Task.WhenAll(tasks);
            }
            
        
            return Ok(equipes);
        }

        // PUT: api/Clubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub([FromRoute] string id, [FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != club.idClub)
            {
                return BadRequest();
            }

            Context.Entry(club).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        // POST: api/Clubs
        [HttpPost]
        public async Task<IActionResult> PostClub([FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Clubs.Add(club);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.idClub }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = await Context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            Context.Clubs.Remove(club);
            await Context.SaveChangesAsync();

            return Ok(club);
        }

        private bool ClubExists(string id)
        {
            return Context.Clubs.Any(e => e.idClub == id);
        }


    }
}