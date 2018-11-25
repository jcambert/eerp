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
    public interface IJoueurService
    {
        //Task<List<Joueur>> loadListForClubFromSpid(string numero, bool addToDb, Club club);
        // Task<Joueur> LoadDetailJoueur(Joueur joueur);
        Task<List<Partie>> loadJoueurParties(Joueur joueur);
        Task<List<Historique>> loadJoueurHistoriques(Joueur joueur);
        Task<List<Classement>> loadHistoriqueClassement(Joueur joueur);
        List<HistoriquePointDto> ConvertToHistoriquePoint(List<Journee> journees);
        List<HistoriqueVictoireDto> ConvertToHistoriqueVictoire(List<Journee> journees);
        List<HistoriqueDefaiteDto> ConvertToHistoriqueDefaite(List<Journee> journees);
        Task<List<Joueur>> LoadJoueurs(Club club);
        Task<Joueur> LoadJoueur(string licence);
    }

    public class JoueurService : ServiceBase, IJoueurService
    {
        public IClubService ClubService { get; }

        public JoueurService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService, IClubService clubService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
            ClubService = clubService;
        }

        private async Task<Joueur> LoadDetailJoueurFromSpid(Joueur joueur, bool autoSave = true)
        {
           
            return await this.InternalLoadFromSpid<ListeJoueurHeader, JoueurDto, Joueur>($"api/joueur/{joueur.Licence}", true, liste => liste?.Liste?.Joueur, (ctx, j) => ctx.Joueurs.Add(j), null, null, autoSave);
        }

        public async Task<List<Joueur>> loadJoueursFromSpid(Club club, bool autoSave = true)
        {
            return await this.InternalLoadListFromSpid<ListeJoueursHeader, List<JoueurDto>, Joueur>($"api/joueur/liste/club/{club.Numero}", true, liste => liste?.Liste?.Joueurs, (ctx,j)=>ctx.Joueurs.Add(j),null,null,autoSave);
        }

        public async Task<List<Partie>> loadJoueurParties(Joueur joueur)
        {
            return await this.InternalLoadListFromSpid<ListePartiesHeader, List<PartieDto>, Partie>($"api/joueur/{joueur.Licence}/parties", false, liste => liste?.Liste?.Parties);
        }

        public async Task<List<Historique>> loadJoueurHistoriques(Joueur joueur)
        {
            return await this.InternalLoadListFromSpid<ListePartiesHeader, List<PartieHistoDto>, Historique>($"api/joueur/{joueur.Licence}/parties/historique", false, liste => liste?.Liste?.Historiques);
        }

        public async Task<List<Classement>> loadHistoriqueClassement(Joueur joueur)
        {
            return await this.InternalLoadListFromSpid<ListeClassementHeader, List<ClassementDto>, Classement>($"api/joueur/{joueur.Licence}/histoclass", false, liste => liste?.Liste?.Classements);
        }

        public List<HistoriquePointDto> ConvertToHistoriquePoint(List<Journee> journees)
        {
            return Mapper.Map<List<HistoriquePointDto>>(journees);
        }

        public List<HistoriqueVictoireDto> ConvertToHistoriqueVictoire(List<Journee> journees)
        {
            return Mapper.Map<List<HistoriqueVictoireDto>>(journees);
        }

        public List<HistoriqueDefaiteDto> ConvertToHistoriqueDefaite(List<Journee> journees)
        {
            return Mapper.Map<List<HistoriqueDefaiteDto>>(journees);
        }

        public async Task<List<Joueur>> LoadJoueurs(Club club)
        {
            var joueurs = DbContext.Joueurs.Where(j => j.NumeroClub == club.Numero).ToList();
            if (joueurs == null || joueurs.Count == 0)
            {
                joueurs = await loadJoueursFromSpid(club,false);
                List<Task> tasks = new List<Task>();
                joueurs.ForEach(joueur => tasks.Add(LoadDetailJoueurFromSpid(joueur,false)));
                await Task.WhenAll(tasks);

                await DbContext.SaveChangesAsync();
            }
            return joueurs.OrderBy(j=>j.Nom).ThenBy(j=>j.Prenom).ToList();
        }

        public async Task<Joueur> LoadJoueur(string licence)
        {
            var joueur = await DbContext.Joueurs.FindAsync(licence);
            if (joueur == null)
                joueur = await LoadJoueurFromSpid(licence);
            return joueur;
        }

        private async Task<Joueur> LoadJoueurFromSpid(string licence)
        {
            var uri = $"/api/joueur/{licence}/spid";
            Func<Joueur, Task> beforeAdd = async (j) =>
               {
                   j.Extra = new JoueurExtra() { Licence = j.Licence };
                  //j.Club = await ClubService.LoadClub(j.NumeroClub, false);
                  await LoadDetailJoueurFromSpid(j, false);
               };
            return await this.InternalLoadFromSpid<ListeJoueurHeader, JoueurDto, Joueur>(uri, true, liste => liste?.Liste?.Joueur, (ctx, j) => { ctx.Joueurs.Add(j); }, null, beforeAdd);
        }
    }
}
