using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Intranet.Api.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.Controllers
{
    public class BaseController:ControllerBase
    {
        public BaseController(IntranetDbContext context, IOptions<ApiSettings> settings, IMapper mapper)
        {
            Context = context;
            Settings = settings.Value;
            Mapper = mapper;
        }

        public IntranetDbContext Context { get; }
        public ApiSettings Settings { get; }
        public IMapper Mapper { get; }
    }

    public abstract class ApiServiceBaseController<TService,TModel> : ControllerBase
        where TService: BaseService<TModel>
        where TModel:IdTrackable
    {
        public TService Service { get; }
        public IntranetDbContext Context => Service.Context;
        public IRepository<IntranetDbContext, TModel> Repository => Service.Repository;

        public ApiServiceBaseController(TService service)
        {
            Service = service;
        }

        // GET: api/Parametres
        [HttpGet]
        public IEnumerable<TModel> GetModel()
        {
            return Repository.Get();
        }

        
        // GET: api/Parametres/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetModel([FromRoute] int id)
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
        
        // PUT: api/Parametres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel([FromRoute] int id, [FromBody] TModel model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            Repository.Update(model);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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
        public async Task<IActionResult> PostModel([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Repository.Insert(model);

            await Context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.Id }, model);
        }

        // DELETE: api/Parametres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var parametre = await Repository.DeleteAsync(id);

            if (parametre == null)
            {
                return NotFound();
            }

            await Context.SaveChangesAsync();

            return Ok(parametre);
        }

        private bool ModelExists(int id)
        {
            return Repository.Exists(id);
        }
    }
}
