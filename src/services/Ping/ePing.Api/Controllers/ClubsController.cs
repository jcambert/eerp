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
    public class ClubsController : ControllerBase
    {
        private readonly PingDbContext _context;
        private readonly IClubService _service;

        public ClubsController(PingDbContext context, IClubService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Clubs
        [HttpGet]
        public IEnumerable<Club> GetClubs()
        {
            return _context.Clubs;
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

           

            var club = await _context.Clubs.FindAsync(numero);

            if (club == null)
            {
                
                if (Request.Path.Value.Contains("/load"))
                {
                    club = await _service.loadFromSpid(numero, true);
                }
                if (club == null)return NotFound();
            }

            return Ok(club);
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

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

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

            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return Ok(club);
        }

        private bool ClubExists(string id)
        {
            return _context.Clubs.Any(e => e.idClub == id);
        }


    }
}