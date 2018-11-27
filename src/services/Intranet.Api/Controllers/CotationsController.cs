using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Intranet.Api.dbcontext;
using Intranet.Api.models;

namespace Intranet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotationsController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public CotationsController(IntranetDbContext context)
        {
            _context = context;
        }

        // GET: api/Cotations
        [HttpGet]
        public IEnumerable<Cotation> GetCotations()
        {
            return _context.Cotations.Include(c=>c.Pieces).ThenInclude(p=>p.Formats);
        }

        // GET: api/Cotations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCotation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotation = await _context.Cotations.FindAsync(id);

            if (cotation == null)
            {
                return NotFound();
            }

            return Ok(cotation);
        }

        // PUT: api/Cotations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotation([FromRoute] int id, [FromBody] Cotation cotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cotation.Id)
            {
                return BadRequest();
            }

            _context.Entry(cotation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotationExists(id))
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

        // POST: api/Cotations
        [HttpPost]
        public async Task<IActionResult> PostCotation([FromBody] Cotation cotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cotations.Add(cotation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCotation", new { id = cotation.Id }, cotation);
        }

        // DELETE: api/Cotations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotation = await _context.Cotations.FindAsync(id);
            if (cotation == null)
            {
                return NotFound();
            }

            _context.Cotations.Remove(cotation);
            await _context.SaveChangesAsync();

            return Ok(cotation);
        }

        private bool CotationExists(int id)
        {
            return _context.Cotations.Any(e => e.Id == id);
        }
    }
}