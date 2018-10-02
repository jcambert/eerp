using Microsoft.AspNetCore.Mvc;
using Ping.Api.services;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Ping.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : SpidControllerBase
    {

        public ClubController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {


        }

        [HttpGet("{numero}/licencies")]
        public async Task<ActionResult<string>> GetByClub(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.LicenceJoueur, new NameValueCollection() {
                {"club", numero }
            });
        }

        [HttpGet("{nomornumero}/detail")]
        [HttpGet("{nomornumero}")]
        public async Task<ActionResult<string>> GetByNomOuNumero(string nomornumero)
        {
            int numero;

            if(Request.Path.Value.Contains("detail"))
                return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubDetail, new NameValueCollection() {
                    {"club", nomornumero}
                });
            if (int.TryParse(nomornumero, out numero))
            {
                if (nomornumero.Length == 2)//Département
                    return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                        {"dep", nomornumero }
                    });
                if (nomornumero.Length == 5)//Code Postal
                    return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                        {"code", nomornumero }
                    });

                //Numero du club
                return await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                    {"numero", nomornumero }
                });

            }
            else
            {
                var result = await SpidRequest.Execute(Configuration.ApiName, Configuration.Club, new NameValueCollection() {
                    {"ville", nomornumero}
                });
                
                return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubFFTT, new NameValueCollection() {
                    {"nom", nomornumero }
                });
            }
        }
        /*
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
        */

            /*
        [HttpGet("{numero}/detail")]
        public async Task<ActionResult<string>> GetDetailByNumero(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ClubDetail, new NameValueCollection() {
                {"club", numero}
            });
        }*/

        [HttpGet("{numero}/equipe/{type}")]
        public async Task<ActionResult<string>> GetEquipe(string numero, string @type)
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