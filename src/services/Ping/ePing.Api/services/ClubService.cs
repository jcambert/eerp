using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ePing.Api.services
{

    public interface IClubService
    {
        Task<Club> LoadClubFromSpid(string numero, bool addIfnotExist);
        Task<List<Equipe>> LoadEquipes(Club club, string @type = null);
        Task<Club> LoadClub(string numero);
    }
    public class ClubService : ServiceBase, IClubService
    {
        public QueryService QueryService { get; }
        public IOrganismeService OrganismeService { get; }
        public ClubService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService, QueryService queryService,IOrganismeService organismeService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
            QueryService = queryService;
            OrganismeService = organismeService;
        }
        public async Task<Club> LoadClubFromSpid(string numero, bool addToDb)
        {
            return await this.InternalLoadFromSpid<ListeClubHeader, ClubDto, Club>($"/api/club/{numero}/detail", true, liste => liste.Liste.Club, (ctx, model) => { ctx.Clubs.Add(model); });
        }

        public async Task<List<Equipe>> LoadEquipesFromSpid(List<Organisme> organismes, Club club, string @type = null)
        {
            Action<Equipe> updateEquipe = equipe =>
            {
                var queries = QueryService.Parse(equipe.LienDivision);
                equipe.IdPoule = int.Parse(queries["cx_poule"]);
                equipe.IdDivision = int.Parse(queries["D1"]);
                equipe.IdOrganismePere = queries["organisme_pere"];
                equipe.Club = club;
                equipe.Type = @type;
                equipe.Organisme = organismes.Where(o => o.Identifiant == equipe.IdOrganismePere).FirstOrDefault();
            };
            string url = $"api/club/{club.Numero}/equipes";
            if (type != null)
                url += $"/{@type}";
            var equipes = await this.InternalLoadListFromSpid<ListeEquipeHeader, List<EquipeDto>, Equipe>(url, true, liste => liste?.Liste?.Equipes,(ctx,e)=>ctx.Equipes.Add(e),null,e=> { updateEquipe(e); });

           // equipes.ForEach(equipe => updateEquipe(equipe));
            return equipes;
        }

        public async Task<List<Equipe>> LoadEquipes(Club club, string @type = null)
        {
            var organismes = await OrganismeService.Load();

            var equipes = DbContext.Equipes.Where(e => e.Club == club).ToList();
            if (equipes == null || equipes.Count == 0)
                equipes =await LoadEquipesFromSpid(organismes, club, @type);
           
            return equipes;
        }

        public async Task<Club> LoadClub(string numero)
        {
            var club = await DbContext.Clubs.FindAsync(numero);
            if(club==null)
                club = await LoadClubFromSpid(numero, true);
            return club;
        }
    }
}
