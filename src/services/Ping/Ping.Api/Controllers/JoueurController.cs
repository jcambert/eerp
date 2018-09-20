using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : ControllerBase
    {

        private readonly IHttpClientFactory _clientFactory;
        public JoueurController(IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;

        }

        [HttpGet("club/{numero}")]
        public async Task<ActionResult<string>> GetByClub(string numero)
        {
            var client = _clientFactory.CreateClient("ping");
            var uri ="/mobile/pxml/xml_licence_c.php".BuildUri(new NameValueCollection() {
                {"serie" , "LAWRSAVOEJZXALD" },
                //{"tm","20180919171858309" },
                {"tm",Now() },
                {"tmc", "bde40f00237c4fff59839f4650e95685c8bcbe5a" },
                {"id", "AW001" },
                {"club", numero }
            });

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            XmlDocument doc = new XmlDocument();
            doc.Load(result);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }

        static string Now() => DateTime.Now.ToString("yyyyMMddhhmmFFF");
        
    }

    public static class UriExtension
    {
        public static string BuildUri(this string root, NameValueCollection query)
        {
            var collection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var key in query.Cast<string>().Where(key => !string.IsNullOrEmpty(query[key])))
            {
                collection[key] = query[key];
            }

            if (root.Contains("?"))
            {
                if (root.EndsWith("&"))
                {
                    root = root + collection.ToString();
                }
                else
                {
                    root = root + "&" + collection.ToString();
                }
            }
            else
            {
                root = root + "?" + collection.ToString();
            }

            return root;
        }
    }
}