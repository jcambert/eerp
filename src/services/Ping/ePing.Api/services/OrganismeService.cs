using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public interface IOrganismeService
    {
        Task<List<Organisme>> LoadFromSpid(bool force=false);
    }
    public class OrganismeService : ServiceBase, IOrganismeService
    {
        public OrganismeService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService,QueryService queryService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
            QueryService = queryService;
        }

        public QueryService QueryService { get; }

        public async Task<List<Organisme>> LoadFromSpid(bool force = false)
        {
            try
            {
                if (force)
                    return await InternalLoadFromSpid();
                else
                {
                    var result = await DbContext.Organisme.CountAsync();

                    if (result == 0)
                        return await InternalLoadFromSpid();
                }
            }
            catch
            {
                
            }

            return new List<Organisme>();
        }

        internal async Task<List<Organisme>> InternalLoadFromSpid()
        {

           
            List<Organisme> organismes = new List<Organisme>();
            string[] types = new string[] { "D", "L", "Z"/*, "F"*/ };
            foreach (var @type in types)
            {
                organismes.AddRange( await this.InternalLoadListFromSpid<ListeOrganismesHeader,List< OrganismeDto>, Organisme>($"/api/organisme/{@type}", true, liste => liste?.Liste?.Organismes, (ctx, model) => {   ctx.Organisme.Add(model); },null,model=> { model.Id = $"{@type}-{model.Identifiant}"; }));
            }

            organismes.Add(    await this.InternalLoadFromSpid<ListeOrganismeHeader, OrganismeDto, Organisme>($"/api/organisme/F", true, liste => liste?.Liste?.Organisme, (ctx, model) => { ctx.Organisme.Add(model); }, null, model => model.Id = $"F-{model.Identifiant}"));
            return organismes;
        }
    }
}
