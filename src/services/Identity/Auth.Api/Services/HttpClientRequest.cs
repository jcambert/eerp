using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public class HttpClientRequest
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public HttpClientRequest(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _mapper = mapper;
        }

        public IHttpClientFactory ClientFactory => _clientFactory;

        public IConfiguration Configuration => _configuration;

        public IMapper Mapper => _mapper;
        

        public HttpClient CreateClient(string clientName) => ClientFactory.CreateClient(Configuration[clientName]);

        protected async Task<TModelDto> InternalExecuteGet<TModelDto>(string clientName, string endPoint, NameValueCollection parameters = null)
        {
            var uri = endPoint.BuildUri(parameters);

            var client = CreateClient(clientName);

            var res = await client.GetStreamAsync(uri);

            StreamReader reader = new StreamReader(res);
            string text = reader.ReadToEnd();
            TModelDto dto = JsonConvert.DeserializeObject<TModelDto>(text);
            return dto;
        }

        public async Task<TModel> ExecuteGet<TModelDto, TModel>(string clientName, string endPoint, NameValueCollection parameters = null)
        {

            TModelDto dto = await InternalExecuteGet<TModelDto>(clientName, endPoint, parameters);
            if (dto == null) return default(TModel);
            var model = Mapper.Map<TModel>(dto);

            return model;
        }


    }
}
