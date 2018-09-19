using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Ping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public ClubController(IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;

        }

        [HttpGet("numero/{numero}")]
        public async Task<ActionResult<string>> GetByNumero(string numero)
        {
            return await searchBy("numero", numero);
            /*
            var client = _clientFactory.CreateClient("ping");

            var formVars = new Dictionary<string, string>();
            formVars.Add("numero", id);
            formVars.Add("rmode", "XML");
            var content = new FormUrlEncodedContent(formVars);
            var response = await client.PostAsync("/rechercheclub/recup_club.php", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            XmlDocument doc = new XmlDocument();
            doc.Load(result);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;*/

        }
        [HttpGet("{nom}")]
        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<string>> GetByNom(string nom)
        {
            return await searchBy("nom", nom);
        }
        private async Task<string> searchBy(string key, string value)
        {
            var client = _clientFactory.CreateClient("ping");

            var formVars = new Dictionary<string, string>();
            formVars.Add(key, value);
            formVars.Add("rmode", "XML");
            var content = new FormUrlEncodedContent(formVars);
            var response = await client.PostAsync("/rechercheclub/recup_club.php", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            XmlDocument doc = new XmlDocument();
            doc.Load(result);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }
    }
}