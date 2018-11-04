using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{

    public interface IChampionnatService
    {
        Task<List<ResultatRencontre>> GetResultats(Equipe equipe);
        Task<List<ResultatRencontre>> GetResultats(string division, string poule);
        Task<List<ResultatRencontre>> GetResultats(int division, int poule);
        Task<List<ClassementEquipe>> GetClassements(Equipe equipe);
        Task<List<ClassementEquipe>> GetClassements(string division, string poule);
        Task<List<ClassementEquipe>> GetClassements(int division, int poule);
    }
    public class ChampionnatService : ServiceBase, IChampionnatService
    {
        public ChampionnatService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
        }

        public async Task<List<ClassementEquipe>> GetClassements(string division, string poule)
        {
            var uri = $"api/championnat/{division}/{poule}/classement";
            return await this.InternalLoadListFromSpid<ListeClassementsEquipeHeader, List<ClassementEquipeDto>, ClassementEquipe>(uri, false, liste => liste?.Liste?.Classements);
        }

        public async Task<List<ClassementEquipe>> GetClassements(int division, int poule)
        {
            return await GetClassements(division.ToString(), poule.ToString());
        }

        public async Task<List<ClassementEquipe>> GetClassements(Equipe equipe)
        {
            var result = await GetClassements(equipe.IdDivision, equipe.IdPoule);
            equipe.Classements = result;
            return result;
        }

        public async Task<List<ResultatRencontre>> GetResultats(string division, string poule)
        {
            var uri = $"api/championnat/{division}/{poule}/resultat";
            return await this.InternalLoadListFromSpid<ListeResultatsRencontresHeader, List<ResultatRencontreDto>, ResultatRencontre>(uri, false, liste => liste?.Liste?.Resultats);
        }

        public async Task<List<ResultatRencontre>> GetResultats(int division, int poule)
        {
            return await GetResultats(division.ToString(), poule.ToString());
        }

        public async Task<List<ResultatRencontre>> GetResultats(Equipe equipe)
        {
            var result = await GetResultats(equipe.IdDivision, equipe.IdPoule);
            equipe.Resultats = result;

            var rs = equipe.Resultats.Where(r => (r.EquipeA == equipe.Nom || r.EquipeB == equipe.Nom) && (r.ScoreA != null && r.ScoreB != null && r.ScoreA > 0 && r.ScoreB > 0));
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
                    equipe.NombreMatchGagne += (resultat.ScoreA < resultat.ScoreB) ?1 : 0;
                    equipe.NombreMatchNul += (resultat.ScoreA == resultat.ScoreB) ? 1 : 0;
                    equipe.NombreMatchPerdu += (resultat.ScoreA > resultat.ScoreB) ? 1 : 0;
                }
            }

            equipe.Tours=equipe.Resultats.GroupBy(r => r.DatePrevue, (key, resultats) => new Tour {Date=key,Resultats= resultats.ToList()});
            return result;
        }
    }
}
