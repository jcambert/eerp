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
        private readonly CacheService _cache;

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

        public CacheService CacheService => _cache;

        public HttpClient CreateClient() => ClientFactory.CreateClient(Configuration["ping:name"]);

        protected async Task<List<TModel>> InternalLoadListFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<PingDbContext, TModel> add = null, Action<TModel> beforeSave = null, Func<TModel, Task> beforeAdd = null, bool autoSave = true) where TModel : Trackable
        {
            List<TModel> model=null;
            Func<TModel, Task> addOrUpdate = async (_model) =>
             {
                 var existed = await DbContext.Set<TModel>().FindAsync(EfService.GetKeyValues(_model));
                 if (existed != null)
                     DbContext.Entry(existed).CurrentValues.SetValues(_model);
                 else

                     add(DbContext, _model);
             };

            try
            {
                var client = CreateClient();

                var res = await client.GetStreamAsync(uri);

                StreamReader reader = new StreamReader(res);
                string text = reader.ReadToEnd();
                TListDto dto = JsonConvert.DeserializeObject<TListDto>(text);

                if (dto == null) return default(List<TModel>);
                try
                {
                    model = Mapper.Map<List<TModel>>(filter(dto));
                }catch(Exception ex1)
                {
                    var msg = ex1.Message;
                }
                try
                {
                    var toto = (filter(dto) as List<dto.ResultatRencontreDto>)[12];
                    var titi = Mapper.Map<ResultatRencontre>(toto);
                }
                catch  { }

                if (model is IEnumerable<TModel>)
                    foreach (var item in model as IEnumerable<TModel>)
                    {
                        if (beforeAdd != null)
                            await beforeAdd(item);
                        if (addToDb)
                        {
                            await addOrUpdate(item);
                            if (beforeSave != null)
                                beforeSave(item);
                        }
                    }


                if (autoSave)
                    try
                    {
                        await DbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        throw ex;
                    }

            }
            catch (Exception ex0)
            {
                var msg = ex0.Message;
                //throw ex0;
            }


            return model;
        }
        protected async Task<TModel> InternalLoadFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<PingDbContext, TModel> add = null, Action<TModel> beforeSave = null, Func<TModel, Task> beforeAdd = null, bool autoSave = true) where TModel : Trackable
        {
            TModel model;
            Func<TModel, Task> addOrUpdate = async (_model) =>
             {
                 var existed = await DbContext.Set<TModel>().FindAsync(EfService.GetKeyValues(_model));
                 if (existed != null)
                     DbContext.Entry(existed).CurrentValues.SetValues(_model);
                 else
                 {
                     if (add != null)
                         add(DbContext, _model);
                     else
                         await DbContext.Set<TModel>().AddAsync(_model);
                 }
             };
            try
            {
                var client = CreateClient();

            var res = await client.GetStreamAsync(uri);

            StreamReader reader = new StreamReader(res);
            string text = reader.ReadToEnd();

            TListDto dto = JsonConvert.DeserializeObject<TListDto>(text);

            if (dto == null) return default(TModel);
            model = Mapper.Map<TModel>(filter(dto));
            if (addToDb && model != null)
            {
                if (beforeAdd != null)
                    await beforeAdd(model);
                await addOrUpdate(model);

                if (beforeSave != null)
                    beforeSave(model);

                if (autoSave)
                    
                        await DbContext.SaveChangesAsync();
                    

            }
            else if (beforeAdd != null)
                await beforeAdd(model);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw ex;
            }
            return model;
        }




    }
}