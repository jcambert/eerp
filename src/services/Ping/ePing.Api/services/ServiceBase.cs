using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public abstract class ServiceBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly PingContext _dbcontext;
        private readonly IMapper _mapper;

        public ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingContext dbcontext)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public IHttpClientFactory ClientFactory => _clientFactory;

        public IConfiguration Configuration => _configuration;

        public PingContext DbContext => _dbcontext;

        public IMapper Mapper => _mapper;

        public HttpClient CreateClient()=> ClientFactory.CreateClient(Configuration["ping:name"]);

        protected async Task<TModel> InternalLoadFromSpid<TListDto,TModelDto,TModel>(string uri, bool addToDb,Func<TListDto, TModelDto> filter,Action<PingContext, TModel>add,Action<TModel> beforeSave =null ) 
        {
            var client = CreateClient();
            
            var res = await client.GetStreamAsync(uri);

            StreamReader reader = new StreamReader(res);
            string text = reader.ReadToEnd();
            TListDto dto = JsonConvert.DeserializeObject<TListDto>(text);

            if (dto == null) return default(TModel);
            var model = Mapper.Map<TModel>(filter(dto));
            if (addToDb)
            {
                add(DbContext, model);
                if (beforeSave != null)
                    beforeSave(model);
                await DbContext.SaveChangesAsync();
            }

            return model;
        }
    }
}
