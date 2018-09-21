using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Ping.Api.services
{
    public interface ISpidRequest
    {
        /// <summary>
        /// Execute Spid Api Request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="api"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<ActionResult<string>> Execute(string name, string api, NameValueCollection parameters);

        Task<ActionResult<string>> ExecuteApi(string name, string endpoint, NameValueCollection parameters);

        Task<ActionResult<string>> ExecuteFFTT(string name, string endpoint, Dictionary<string, string> parameters);
    }

    public class SpidRequest : ISpidRequest
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly Dictionary<string, string> apis = new Dictionary<string, string>();

        public SpidRequest(  IHttpClientFactory clientFactory, ISpidLicence licence,ISpidConfiguration configuration)
        {
            Configuration = configuration;
            _clientFactory = clientFactory;
            Licence = licence;


            ConfigureApis();
            

        }
        public ISpidConfiguration Configuration { get;  }

        public ISpidLicence Licence { get;  }

        private void ConfigureApis()
        {
            apis[Configuration.LicenceJoueur] = "spid:api:licence_c";
            apis[Configuration.Departement] = "spid:api:club_dep";
            apis[Configuration.Club] = "spid:api:club";
            apis[Configuration.ClubDetail] = "spid:api:club_detail";
            apis[Configuration.ClubFFTT] = "spid:fftt:recup_club";
            apis[Configuration.Organisme] = "spid:api:organisme";
        }

        public async Task<ActionResult<string>> Execute(string name,string api, NameValueCollection parameters)
        {
            if (apis[api].StartsWith("spid:api"))
                return await ExecuteApi(name, Configuration[apis[api]], parameters as NameValueCollection);
            else if (apis[api].StartsWith("spid:fftt"))
            {
                Dictionary<string, string> @params = new Dictionary<string, string>();
                foreach (string key in parameters.Keys)
                {
                    @params[key] = parameters[key];
                }
                return await ExecuteFFTT(name, Configuration[apis[api]], @params);
            }
            return "";
        }

        public async Task<ActionResult<string>> ExecuteApi(string name, string endpoint, NameValueCollection parameters)
        {
            
            var client = _clientFactory.CreateClient(name);
            parameters.Set("id", Configuration.ApplicationId);
            parameters.Set("serie", Configuration.Serie);
            parameters.Set("tm", Licence.Tm);
            parameters.Set("tmc", Licence.Tmc);
            var uri = Configuration.ApiEndpoint.FormatMe( endpoint).BuildUri(parameters);

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            XmlDocument doc = new XmlDocument();
            doc.Load(result);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }

        public async Task<ActionResult<string>> ExecuteFFTT(string name,string endpoint, Dictionary<string,string> parameters)
        {
            var client = _clientFactory.CreateClient(name);

            parameters.Add("rmode", "XML");
            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync($"{endpoint}.php", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            XmlDocument doc = new XmlDocument();
            doc.Load(result);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }

    }
}
