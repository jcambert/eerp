using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Intranet.Api.services;

namespace Intranet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotationsController : ApiServiceBaseController<BaseService<Cotation>,Cotation>
    {


        public CotationsController(BaseService<Cotation> service):base(service)
        {
        }

        // GET: api/Parametres
        [HttpGet("{reference}/{indice}/{version?}")]
        public IActionResult GetByReference([FromRoute] string reference,[FromRoute]string indice,[FromRoute]int version=0)
        {
            var result=Repository.Get(p => p.Reference == reference && p.Indice == indice && p.Version == version).FirstOrDefault();

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}