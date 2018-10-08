using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public abstract class ServiceBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly PingDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly EfService _efService;

        public ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _dbcontext = dbcontext;
            _mapper = mapper;
            _efService = efService;
        }

        public IHttpClientFactory ClientFactory => _clientFactory;

        public IConfiguration Configuration => _configuration;

        public PingDbContext DbContext => _dbcontext;

        public IMapper Mapper => _mapper;

        public EfService EfService => _efService;

        public HttpClient CreateClient() => ClientFactory.CreateClient(Configuration["ping:name"]);

        protected async Task<List<TModel>> InternalLoadListFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<PingDbContext, TModel> add=null, Action<TModel> beforeSave = null) where TModel : Trackable
        {
            List<TModel> model;
            Action<TModel> addOrUpdate = async (_model) =>
            {
                var existed = await DbContext.Set<TModel>().FindAsync(EfService.GetKeyValues(_model));
                if (existed != null)
                    DbContext.Entry(existed).CurrentValues.SetValues(_model);
                else
                    add(DbContext, _model);
            };

            var client = CreateClient();

            var res = await client.GetStreamAsync(uri);

            StreamReader reader = new StreamReader(res);
            string text = reader.ReadToEnd();
            TListDto dto = JsonConvert.DeserializeObject<TListDto>(text);

            if (dto == null) return default(List<TModel>);
            model = Mapper.Map<List<TModel>>(filter(dto));
            if (addToDb)
            {
                if (model is IEnumerable<TModel>)
                    foreach (var item in model as IEnumerable<TModel>)
                    {
                        addOrUpdate(item);
                        if (beforeSave != null)
                            beforeSave(item);
                    }





                await DbContext.SaveChangesAsync();
            }

            return model;
        }
        protected async Task<TModel> InternalLoadFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<PingDbContext, TModel> add, Action<TModel> beforeSave = null) where TModel : Trackable
        {
            TModel model;
            Action<TModel> addOrUpdate = async (_model) =>
            {
                var existed = await DbContext.Set<TModel>().FindAsync(EfService.GetKeyValues(_model));
                if (existed != null)
                    DbContext.Entry(existed).CurrentValues.SetValues(_model);
                else
                    add(DbContext, _model);
            };

            var client = CreateClient();

            var res = await client.GetStreamAsync(uri);

            StreamReader reader = new StreamReader(res);
            string text = reader.ReadToEnd();
            try
            {
                TListDto dto = JsonConvert.DeserializeObject<TListDto>(text);

                if (dto == null) return default(TModel);
                model = Mapper.Map<TModel>(filter(dto));
                if (addToDb && model!=null)
                {

                    addOrUpdate(model);

                    if (beforeSave != null)
                        beforeSave(model);
                    await DbContext.SaveChangesAsync();
                }

                return model;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                throw ex;
            }
            

            
        }
    }
}
