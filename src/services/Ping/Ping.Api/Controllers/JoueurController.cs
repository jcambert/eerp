using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ping.Api.services;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Ping.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : SpidControllerBase
    {


        public JoueurController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {

        }

        [HttpGet("club/{numero}")]
        public async Task<ActionResult<string>> GetByClub(string numero)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.LicenceJoueur, new NameValueCollection() {
                {"club", numero }
            });
        }


        [HttpGet("liste/club/{club?}")]
        [HttpGet("liste/nom/{nom?}/{prenom?}")]
        [HttpGet("liste/license/{license?}")]
        public async Task<ActionResult<string>> GetList(string club = null, string nom = null,string prenom=null,string license=null)
        {
            NameValueCollection @params = new NameValueCollection();
            if (club != null) @params["club"] = club;
            if (nom != null) @params["nom"] = nom.ToUpper();
            if (prenom != null) @params["prenom"] = prenom;
            if (license != null) @params["licence"] = license;

            return await SpidRequest.Execute(Configuration.ApiName, Configuration.ListeJoueur,@params);
        }

        [HttpGet("{license}")]
        public async Task<ActionResult<string>> GetByLicense(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Joueur, new NameValueCollection() {
                {"licence", license}
            });
        }

        [HttpGet("spid/{license}")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetByLicenseSpid(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.JoueurSpid, new NameValueCollection() {
                {"licence", license}
            });
        }

        [HttpGet("partie/{license}")]
        public async Task<ActionResult<string>> GetByPartieSpid(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.JoueurPartieSpid, new NameValueCollection() {
                {"numlic", license}
            });
        }

        [HttpGet("histo/{license}")]
        public async Task<ActionResult<string>> GetByHistorique(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.HistoriqueClassement, new NameValueCollection() {
                {"numlic", license}
            });
        }
    }

}