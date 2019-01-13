using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{

    public interface IChampionnatService
    {
        Task<List<ResultatRencontre>> LoadResultats( Equipe equipe);
        //Task<List<ResultatRencontre>> GetResultats(string division, string poule);
        //Task<List<ResultatRencontre>> LoadResultats(int division, int poule);
        Task<List<ClassementEquipe>> LoadClassements( Equipe equipe);
        Task<List<ResultatRencontre>> LoadResultats(int division, int poule);
        //Task<List<ClassementEquipe>> LoadClassementsFromSpid(string division, string poule);
        Task<List<ClassementEquipe>> LoadClassements(int division, int poule);
    }
    public class ChampionnatService : ServiceBase, IChampionnatService
    {
        public ChampionnatService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, /*PingDbContext dbcontext,*/ EfService efService) : base(clientFactory, configuration, mapper,/* dbcontext,*/ efService)
        {
        }

        private async Task<List<ClassementEquipe>> LoadClassementsFromSpid(Equipe equipe)
        {
            var uri = $"api/championnat/{equipe.IdDivision}/{equipe.IdPoule}/classement";
            return await this.InternalLoadListFromSpid<ListeClassementsEquipeHeader, List<ClassementEquipeDto>, ClassementEquipe>(uri, true, liste => liste?.Liste?.Classements, (ctx,c)=>ctx.ClassementsEquipes.Add(c),null,async (c )=> await Task.Run(()=> {c.Division = equipe.IdDivision; c.Poule = equipe.IdPoule; }));
        }

        public async Task<List<ClassementEquipe>> LoadClassementsFromDb(Equipe equipe)
        {
            /* var cls = DbContext.ClassementsEquipes.Where(e => e.Division == equipe.IdDivision&& e.Poule == equipe.IdPoule).ToList();
             if (cls == null || cls.Count == 0)
                 cls = await LoadClassementsFromSpid(equipe);
             return cls;*/
            return null;
        }

        public async Task<List<ClassementEquipe>> LoadClassements(Equipe equipe)
        {
            var result = await LoadClassementsFromDb(equipe);
            equipe.Classements = result;
            return result;
        }

        private async Task<List<ResultatRencontre>> LoadResultatsFromSpid(Equipe equipe)
        {
            var uri = $"api/championnat/{equipe.IdDivision}/{equipe.IdPoule}/resultat";
            return await this.InternalLoadListFromSpid<ListeResultatsRencontresHeader, List<ResultatRencontreDto>, ResultatRencontre>(uri, true, liste => liste?.Liste?.Resultats, (ctx, r) => ctx.ResultatsRencontres.Add(r), null, async(r) =>await Task.Run(()=> { r.Division = equipe.IdDivision;r.Poule = equipe.IdPoule;  r.EquipeLibelle = equipe.Libelle; }));
        }


        private async Task<List<ResultatRencontre>> LoadResultatsFromDb(Equipe equipe)
        {
            /*var rs = DbContext.ResultatsRencontres.Where(r => r.Division == equipe.IdDivision && r.Poule == equipe.IdPoule).ToList();
            if (rs == null || rs.Count == 0)
                rs = await LoadResultatsFromSpid(equipe);
            return rs;*/
            return null;
        }

        public async Task<List<ResultatRencontre>> LoadResultats(Equipe equipe)
        {
            var result = await LoadResultatsFromDb(equipe);
            equipe.Resultats = result;

            var rs = equipe.Resultats.Where(r => (r.EquipeA == equipe.Nom || r.EquipeB == equipe.Nom) && (r.ScoreA > 0 && r.ScoreB > 0));
            foreach (var resultat in rs)
            {

                if (resultat.EquipeA == equipe.Nom)
                {
                    equipe.NombreMatchGagne += (resultat.ScoreA > resultat.ScoreB) ? 1 : 0;
                    equipe.NombreMatchNul += (resultat.ScoreA == resultat.ScoreB) ? 1 : 0;
                    equipe.NombreMatchPerdu += (resultat.ScoreA < resultat.ScoreB) ? 1 : 0;
                }

                else if (resultat.EquipeB == equipe.Nom)
                {
                    equipe.NombreMatchGagne += (resultat.ScoreA < resultat.ScoreB) ? 1 : 0;
                    equipe.NombreMatchNul += (resultat.ScoreA == resultat.ScoreB) ? 1 : 0;
                    equipe.NombreMatchPerdu += (resultat.ScoreA > resultat.ScoreB) ? 1 : 0;
                }
            }

            equipe.Tours = equipe.Resultats.OrderBy(r=>r.DatePrevue).GroupBy(r => r.DatePrevue, (key, resultats) => new Tour { Date = key, Resultats = resultats.ToList() });
            return result;
        }

        public async Task<List<ResultatRencontre>> LoadResultats(int division, int poule)
        {
            var uri = $"api/championnat/{division}/{poule}/classement";
            return await this.InternalLoadListFromSpid<ListeResultatsRencontresHeader, List<ResultatRencontreDto>, ResultatRencontre>(uri, true, liste => liste?.Liste?.Resultats);
        }

        public async Task<List<ClassementEquipe>> LoadClassements(int division, int poule)
        {
            var uri = $"api/championnat/{division}/{poule}/classement";
            return await this.InternalLoadListFromSpid<ListeClassementsEquipeHeader, List<ClassementEquipeDto>, ClassementEquipe>(uri, true, liste => liste?.Liste?.Classements);
        }
    }
}
