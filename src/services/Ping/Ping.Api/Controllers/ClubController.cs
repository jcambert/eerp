using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ping.Api.services;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Ping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : SpidControllerBase
    {
        
        public ClubController(ISpidRequest request, ISpidConfiguration configuration):base(request,configuration)
        {


        }

        [HttpGet("numero/{numero}")]
        public async Task<ActionResult<string>> GetByNumero(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                {"numero", numero }
            });

        }
        [HttpGet("{nom}")]
        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<string>> GetByNom(string nom)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubFFTT, new NameValueCollection() {
                {"nom", nom }
            });
        }

        [HttpGet("dept/{dept}")]
        public async Task<ActionResult<string>> GetByDepartement(string dept)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Departement, new NameValueCollection() {
                {"dep", dept }
            });
        }
        
        [HttpGet("cp/{cp}")]
        public async Task<ActionResult<string>> GetByCodePostal(string cp)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                {"code", cp }
            });
        }
        
        [HttpGet("ville/{ville}")]
        public async Task<ActionResult<string>> GetByVille(string ville)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                {"ville", ville}
            });
        }

        [HttpGet("detail/{numero}")]
        public async Task<ActionResult<string>> GetDetailByNumero(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubDetail, new NameValueCollection() {
                {"club", numero}
            });
        }

        [HttpGet("equipe/{numero}/{type}")]
        public async Task<ActionResult<string>> GetEquipe(string numero,string @type)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubEquipe, new NameValueCollection() {
                {"numclu", numero},
                {"type",@type }
            });
        }

        [HttpGet("classement/{division}")]
        public async Task<ActionResult<string>> GetByClassementDivision(string division)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClassementDivision, new NameValueCollection() {
                {"res_division", division}
            });
        }
    }
}