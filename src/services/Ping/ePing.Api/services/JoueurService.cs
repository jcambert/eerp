using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public interface IJoueurService
    {
        Task<List<JoueurSpid>> loadListForClubFromSpid(string numero, bool addToDb, Club club);
        Task<JoueurSpid> loadDetailJoueur(string licence, bool addToDb = false, Club club = null);
        Task<List<Partie>> loadJoueurParties(JoueurSpid joueur);
        Task<List<Historique>> loadJoueurHistoriques(JoueurSpid joueur);
    }

    public class JoueurService : ServiceBase, IJoueurService
    {
        public JoueurService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {

        }

        public async Task<JoueurSpid> loadDetailJoueur(string licence, bool addToDb = false, Club club = null)
        {
            return await this.InternalLoadFromSpid<ListeJoueurHeader, JoueurSpidDto, JoueurSpid>($"api/joueur/{licence}", addToDb, liste => liste?.Liste?.Joueur, (ctx, joueur) => { ctx.JoueurSpid.Add(joueur); }, joueur => joueur.Club = club);
        }

        public async Task<List<JoueurSpid>> loadListForClubFromSpid(string numero, bool addToDb, Club club)
        {
            return await this.InternalLoadListFromSpid<ListeJoueursHeader, List<JoueurSpidDto>, JoueurSpid>($"api/joueur/liste/club/{numero}", addToDb, liste => liste?.Liste?.Joueurs, (ctx, model) => { ctx.JoueurSpid.AddRange(model); }, model => model.Club = club);
        }

        public async Task<List<Partie>> loadJoueurParties(JoueurSpid joueur)
        {
            return await this.InternalLoadListFromSpid<ListePartiesHeader, List<PartieDto>, Partie>($"api/joueur/{joueur.Licence}/parties", false, liste => liste?.Liste?.Parties);
        }

        public async Task<List<Historique>> loadJoueurHistoriques(JoueurSpid joueur)
        {
            return await this.InternalLoadListFromSpid<ListePartiesHeader, List<PartieHistoDto>, Historique>($"api/joueur/{joueur.Licence}/parties/historique", false, liste => liste?.Liste?.Historiques);
        }
    }
}
