using Auth.Api.dto;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Collections.Generic;
using System;

namespace Auth.Api.Services
{
    public class AuthRequest : HttpClientRequest
    {
        const string CONFIG_URI = ".well-known/openid-configuration";
        const string ISSUER = "issuer";
        const string JWKS = "jwks_uri";
        const string AUTHORIZATION = "authorization_endpoint";
        const string TOKEN = "token_endpoint";
        const string USER_INFO = "userinfo_endpoint";
        const string END_SESSION = "end_session_endpoint";

        private bool discovered = false;

       

        public AuthRequest(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper) : base(clientFactory, configuration, mapper)
        {
            
        }
        public DiscoveryResponse disco { get; private set; }

        private async Task Discover(string baseUrl)
        {
            if (discovered) return;
            var client = CreateClient("auth:name");
            client.BaseAddress=new Uri(baseUrl);
            disco = await client.GetDiscoveryDocumentAsync();
            if (disco.IsError) throw new Exception(disco.Error);
            discovered = true;
           
        }
       /* public string GetBaseUrl()
        {
            var request = Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }*/

        public async Task<TokenResponse> Login(string baseUrl,string licence)
        {
            await Discover(baseUrl);

            var client = new HttpClient();

            var resp = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "pingapi",
                UserName = licence,
                Password = "132"
                

            });
            return resp;
        }
    }
}
