using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ePing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ePing.Controllers
{
    
    
    //[Authorize]
    public class LogoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
       // private readonly ApiSettings _settings;
        public LogoutController(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
            //_settings = settings.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromBody]TokenModel model)
        {
            try
            {
                string token=model.token;
                if (token == null)
                {
                    //From Request
                    var reqBearer = this.Request.Headers.Where(h => h.Key == "Authorization").Select(h => new { key = h.Key, value = h.Value }).FirstOrDefault();
                    if (reqBearer != null)
                        token = reqBearer.value;
                }
                //from Session
                if (token == null)
                {
                    string auth = HttpContext.Session.GetValue<string>("auth");

                    var dto = JsonConvert.DeserializeObject<BearerDto>(auth);
                    token = dto.Jwt;

                    
                }


                var endpoint = _configuration["ping:auth:logout_endpoint"];
                endpoint = String.Format(endpoint, token);
                endpoint = "/Logout";
                FormUrlEncodedContent dataForm = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string,string>("token",token)
                });

                HttpClient client = _factory.CreateClient("auth");
                
                var response = await client.PostAsync(endpoint, dataForm);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return Forbid();
                }
                else
                {

                    return Ok();
                }
            }
            catch
            {
                return Ok();
            }
        }
        public class TokenModel
        {
            public string token { get; set; }
        }
    }
}