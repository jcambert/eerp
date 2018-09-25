using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public class SpidRequest<TDbContext> where TDbContext:DbContext
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly TDbContext _dbcontext;
        private readonly IMapper _mapper;

        public SpidRequest(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, TDbContext  dbcontext)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public IHttpClientFactory ClientFactory => _clientFactory;

        public IConfiguration Configuration => _configuration;

        public TDbContext DbContext => _dbcontext;

        public IMapper Mapper => _mapper;

        public HttpClient CreateClient() => ClientFactory.CreateClient(Configuration["ping:name"]);

        public async Task<TModel> LoadFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<TDbContext, TModel> add, Action<TModel> beforeSave = null)
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
