using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Api.Services
{


    public class SpidRequest<TDbContext> : HttpClientRequest where TDbContext : DbContext
    {

        private readonly TDbContext _dbcontext;

        public SpidRequest(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, TDbContext dbcontext) : base(clientFactory, configuration, mapper)
        {
            _dbcontext = dbcontext;
        }

        public TDbContext DbContext => _dbcontext;

        public async Task<TModel> LoadFromSpid<TListDto, TModelDto, TModel>(string uri, bool addToDb, Func<TListDto, TModelDto> filter, Action<TDbContext, TModel> add = null, Action<TModel> beforeSave = null) where TModel : IdentityUser
        {
            TListDto dto = await InternalExecuteGet<TListDto>("ping:name", uri);

            if (dto == null) return default(TModel);
            var model = Mapper.Map<TModel>(filter(dto));
            if (addToDb)
            {
                if (add == null) throw new ArgumentNullException("The 'add' parameter must be set when 'addToDb'=true");
                var existed = await DbContext.Set<TModel>().FindAsync(model.Id);
                if (existed != null)
                    DbContext.Entry(existed).CurrentValues.SetValues(model);
                else
                    add(DbContext, model);
                if (beforeSave != null)
                    beforeSave(model);
                await DbContext.SaveChangesAsync();
            }

            return model;
        }
    }
}
