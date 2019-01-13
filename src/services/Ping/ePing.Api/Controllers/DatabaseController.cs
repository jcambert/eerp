using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePing.Api.dbcontext;
using ePing.Api.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePing.Api.Controllers
{
    [Route("api/db")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        public DatabaseController(/*PingDbContext context,*/ IClubService service)
        {
           // Context = context;
            Service = service;
        }

       // public PingDbContext Context { get; }
        public IClubService Service { get; }

        [HttpPost]
        public async Task<IActionResult> reset()
        {
           /* Context.Clubs.RemoveRange(Context.Clubs);
            await Context.SaveChangesAsync();*/
            return Ok();
        }

    }
}