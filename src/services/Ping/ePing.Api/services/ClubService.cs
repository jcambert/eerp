using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{

    public interface IClubService
    {
        Task<Club> loadFromSpid(string numero, bool addIfnotExist);
        Task<List<Equipe>> loadEquipes(string numero, string @type = null);
    }
    public class ClubService : ServiceBase, IClubService
    {
        public QueryService QueryService { get; }

        public ClubService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService, QueryService queryService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
            QueryService = queryService;
        }
        public async Task<Club> loadFromSpid(string numero, bool addToDb)
        {
            return await this.InternalLoadFromSpid<ListeClubHeader, ClubDto, Club>($"/api/club/{numero}/detail", true, liste => liste.Liste.Club, (ctx, model) => { ctx.Clubs.Add(model); });
        }
        public async Task<List<Equipe>> loadEquipes(string numero, string @type = null)
        {
            Action<Equipe> updateEquipe = equipe =>
            {
                var queries = QueryService.Parse(equipe.LienDivision);
                equipe.IdPoule = int.Parse(queries["cx_poule"]);
                equipe.IdDivision = int.Parse(queries["D1"]);
                equipe.IdOrganismePere = queries["organisme_pere"];
            };
            string url = $"api/club/{numero}/equipes";
            if (type != null)
                url += $"/{@type}";
            var equipes = await this.InternalLoadListFromSpid<ListeEquipeHeader, List<EquipeDto>, Equipe>(url, false, liste => liste?.Liste?.Equipes);

            equipes.ForEach(equipe => updateEquipe(equipe));
            return equipes;
        }


    }
}
