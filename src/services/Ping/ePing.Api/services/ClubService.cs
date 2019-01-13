using AutoMapper;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{

    public interface IClubService
    {
        //Task<Club> LoadClubFromSpid(string numero, bool addIfnotExist);
        Task<List<Equipe>> LoadEquipes(Club club, string @type = null);
        Task<Club> LoadClub(string numero, bool autoSave = true);
        Task<List<ClubSearch>> SearchClub(string numero);
        Task<List<Joueur>> LoadJoueurs(Club club);
    }
    public class ClubService : ServiceBase, IClubService
    {
        private ElasticService _elastic;

        public IJoueurService JoueurService { get; }
        public QueryService QueryService { get; }
        public IOrganismeService OrganismeService { get; }
        public ElasticClient Elastic
        {
            get { return _elastic.Elastic; }
        }
        public ClubService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, /*PingDbContext dbcontext, */EfService efService, QueryService queryService, IOrganismeService organismeService, ElasticService elastic,IJoueurService joueurService) : base(clientFactory, configuration, mapper, /*dbcontext,*/ efService)
        {
            QueryService = queryService;
            OrganismeService = organismeService;
            _elastic = elastic;
            JoueurService = joueurService;
        }


        private async Task<Club> LoadClubFromSpid(string numero, bool autoSave = true)
        {
            return await this.InternalLoadFromSpid<ListeClubHeader, ClubDto, Club>($"/api/club/{numero}/detail", true, liste => liste.Liste.Club, (ctx, model) => { ctx.Clubs.Add(model); }, null, null, autoSave);
        }

        private async Task<List<ClubSearch>> SearchClubFromSpid(string numero)
        {
            Func<string, ListeClubsSearchHeader> onUniqueResultat = (text) =>
            {
                var liste = JsonConvert.DeserializeObject<ListeClubSearchHeader>(text);
                var result0 = new ListeClubsSearchHeader();
                result0.Liste = new ClubsSearchDtoHeader();
                result0.Liste.Clubs.Add(liste.Liste.Club);
                return result0;
            };
            return await this.InternalLoadListFromSpid<ListeClubsSearchHeader, List<ClubSearchDto>, ClubSearch>($"/api/club/{numero}", false, liste => liste?.Liste?.Clubs, null, null, null, false, onUniqueResultat);
        }

        public async Task<List<Equipe>> LoadEquipesFromSpid(List<Organisme> organismes, Club club, string @type = null)
        {
            Action<Equipe> updateEquipe = equipe =>
            {
                var queries = QueryService.Parse(equipe.LienDivision);
                equipe.IdPoule = int.Parse(queries["cx_poule"]);
                equipe.IdDivision = int.Parse(queries["D1"]);
                equipe.IdOrganismePere = queries["organisme_pere"];
                //equipe.Club = club;
                equipe.Type = @type;
                equipe.Organisme = organismes.Where(o => o.Identifiant == equipe.IdOrganismePere).FirstOrDefault();
            };
            string url = $"api/club/{club.Numero}/equipes";
            if (type != null)
                url += $"/{@type}";
            var equipes = await this.InternalLoadListFromSpid<ListeEquipeHeader, List<EquipeDto>, Equipe>(url, true, liste => liste?.Liste?.Equipes, (ctx, e) => ctx.Equipes.Add(e), null, async (e) => await Task.Run(() => { updateEquipe(e); }));

            // equipes.ForEach(equipe => updateEquipe(equipe));
            return equipes;
        }

        public async Task<List<Equipe>> LoadEquipes(Club club, string @type = null)
        {

            List<Equipe> equipes = null;
            if (!club.Equipes.Any())
            {
                try
                {
                    var organismes = await OrganismeService.Load();
                    equipes = await LoadEquipesFromSpid(organismes, club, @type);
                    club.Equipes = equipes;
                    var resp = await Elastic.UpdateAsync(new DocumentPath<Club>(club), u => u.Doc(club));
                }
                catch (Exception e)
                {

                }

            }
            /* var equipes = DbContext.Equipes.Where(e => e.Club == club).ToList();
             if (equipes == null || equipes.Count == 0)
                 equipes =await LoadEquipesFromSpid(organismes, club, @type);
            */
            return equipes;
        }

        public async Task<Club> LoadClub(string numero, bool autoSave = true)
        {
            Club club = null;


            //var resp=await Elastic.SearchAsync<Club>(s=>s.Query(q=>q.Match(m=>m.Field(f=>f.Numero).Query(numero))));
            var resp = await Elastic.GetAsync<Club>(new DocumentPath<Club>(numero));
            // if (resp.IsValid && resp.Hits.Count == 1)
            if (resp.IsValid && resp.Found)
                //club = resp.Hits.First().Source;
                club = resp.Source;
            else
            {
                //club = await DbContext.Clubs.FindAsync(numero);
                //if (club == null)
                club = await LoadClubFromSpid(numero, autoSave);
                if (resp.ApiCall.HttpStatusCode == 404)
                    await Elastic.IndexDocumentAsync<Club>(club);
            }

            return club;
        }

        public async Task<List<ClubSearch>> SearchClub(string numero)
        {
            return await SearchClubFromSpid(numero);
        }

        public async Task<List<Joueur>> loadJoueursFromSpid(Club club, bool addToDb = true, bool autoSave = true)
        {
            return await this.InternalLoadListFromSpid<ListeJoueursHeader, List<JoueurDto>, Joueur>($"api/joueur/liste/club/{club.Numero}", addToDb, liste => liste?.Liste?.Joueurs, (ctx, j) => ctx.Joueurs.Add(j), null, null, autoSave);
        }
        public async Task<List<Joueur>> LoadJoueurs(Club club)
        {
            List<Joueur> joueurs = null;
            if (!club.Joueurs.Any())
            {
                joueurs = await loadJoueursFromSpid(club, false, false);
                club.Joueurs = new List<string>();
                joueurs.ForEach(j =>
                {
                    club.Joueurs.Add(j.Licence);
                });
                var resp = await Elastic.UpdateAsync(new DocumentPath<Club>(club), u => u.Doc(club));
            }
            //var resp=await Elastic.SearchAsync<Club>(s=>s.Query(q=>q.Match(m=>m.Field(f=>f.Numero).Query(numero))));

            var jsResp = await Elastic.SearchAsync<Joueur>(s => s.Query(q => q.MatchPhrase(m => m.Field(f => f.NumeroClub).Query(club.Numero))));
            var hits = jsResp.Hits.ToList();

            List<Task> tasks = new List<Task>();
            club.Joueurs.ForEach(h =>
            {
                tasks.Add(JoueurService.LoadJoueur(h));
            });
            Task.WaitAll(tasks.ToArray());

            jsResp = await Elastic.SearchAsync<Joueur>(s => s.Query(q => q.MatchPhrase(m => m.Field(f => f.NumeroClub).Query(club.Numero))));
            hits = jsResp.Hits.ToList();
            jsResp = await Elastic.SearchAsync<Joueur>(s => s.MatchAll());
            return joueurs;
        }
    }
}
