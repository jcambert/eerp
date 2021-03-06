﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ping.Api.services;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Ping.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : SpidControllerBase
    {


        public JoueurController(ISpidRequest request, ISpidConfiguration configuration) : base(request, configuration)
        {

        }



        [HttpGet("liste/club/{club?}")]
        [HttpGet("liste/nom/{nom?}/{prenom?}")]
        [HttpGet("liste/licence/{licence?}")]
        public async Task<ActionResult<string>> GetList(string club = null, string nom = null,string prenom=null,string licence=null)
        {
            NameValueCollection @params = new NameValueCollection();
            if (club != null) @params["club"] = club;
            if (nom != null) @params["nom"] = nom;
            if (prenom != null) @params["prenom"] = prenom;
            if (licence != null) @params["licence"] = licence;

            var response = await SpidRequest.Execute(Configuration.ApiName, Configuration.ListeJoueur,@params);

            return response;
        }

        [HttpGet("{license}")]
        public async Task<ActionResult<string>> GetByLicense(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.Joueur, new NameValueCollection() {
                {"licence", license}
            });
        }

        [HttpGet("{license}/spid")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetByLicenseSpid(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.JoueurSpid, new NameValueCollection() {
                {"licence", license}
            });
        }

        [HttpGet("{licence}/parties/historique")]
        [HttpGet("{licence}/parties")]
        public async Task<ActionResult<string>> GetByPartieSpid(string licence)
        {
            var resp = "";
            if(Request.Path.Value.Contains("/historique"))
                resp= await SpidRequest.Execute(Configuration.ApiName, Configuration.JoueurPartieMySql, new NameValueCollection() {
                {"licence", licence}
            });
            else
            resp= await SpidRequest.Execute(Configuration.ApiName, Configuration.JoueurPartieSpid, new NameValueCollection() {
                {"numlic", licence}
            });
            return resp;
        }

        [HttpGet("{license}/histoclass")]
        public async Task<ActionResult<string>> GetByHistorique(string license)
        {
            return await SpidRequest.Execute(Configuration.ApiName, Configuration.HistoriqueClassement, new NameValueCollection() {
                {"numlic", license}
            });
        }
    }

}