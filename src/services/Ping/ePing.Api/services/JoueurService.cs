using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        Task<List<Joueur>> LoadJoueursForClub(Club club, bool reload = false);
        Task<Joueur> LoadJoueur(string licence);
        Task<List<JoueurSearch  >> ListeJoueursByClub(string club);
        Task<List<JoueurSearch>> ListeJoueursByNom(string nom, string prenom);
        Task<List<JoueurSearch>> ListeJoueursByLicence(string licence);
    }

    public class JoueurService : ServiceBase, IJoueurService
    {
        public IClubService ClubService { get; }

        public JoueurService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService, IClubService clubService) : base(clientFactory, configuration, mapper, dbcontext, efService)
        {
            ClubService = clubService;
        }

        private async Task<Joueur> LoadDetailJoueurFromSpid(string licence, bool addToDb=true, bool autoSave = true)
        {
            Joueur result =await this.InternalLoadFromSpid<ListeJoueurHeader, JoueurDto, Joueur>($"api/joueur/{licence}", addToDb, liste => liste?.Liste?.Joueur, (ctx, j) => ctx.Joueurs.Add(j), null, null, autoSave);

            return result;
        }

        public async Task<List<Joueur>> loadJoueursFromSpid(Club club, bool addToDb = true, bool autoSave = true)
        {
            return await this.InternalLoadListFromSpid<ListeJoueursHeader, List<JoueurDto>, Joueur>($"api/joueur/liste/club/{club.Numero}", addToDb, liste => liste?.Liste?.Joueurs, (ctx,j)=>ctx.Joueurs.Add(j),null,null,autoSave);
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

        public async Task<List<JoueurSearch>> loadJoueursByNom(string nom,string prenom)
        {
            Func<string, ListeJoueursSearchHeader> onUniqueResultat = (text) =>
            {
                var liste=JsonConvert.DeserializeObject<ListeJoueurSearchHeader>(text);
                var result0 = new ListeJoueursSearchHeader();
                result0.Liste.Joueurs.Add(liste.Liste.Joueur);
                return result0;
            };
            List<JoueurSearch> result = await this.InternalLoadListFromSpid<ListeJoueursSearchHeader, List<JoueurSearchDto>, JoueurSearch>($"api/joueur/liste/nom/{nom}/{prenom}", false, liste => liste?.Liste?.Joueurs, null, null, null,false,onUniqueResultat);
            return result;
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

        public async Task<List<Joueur>> LoadJoueursForClub(Club club,bool reload=false)
        {
            List<Joueur> joueurs = null;
            try
            {


                if (!reload)
                    joueurs = DbContext.Joueurs.Where(j => j.NumeroClub == club.Numero).ToList();
                else
                {
                    DbContext.Joueurs.RemoveRange(DbContext.Joueurs.Where(j => j.NumeroClub == club.Numero).ToList());
                    await DbContext.SaveChangesAsync();
                }
                if (joueurs == null || joueurs.Count == 0)
                {
                    List<Task> tasks = new List<Task>();
                    joueurs = await loadJoueursFromSpid(club, false, false);

                    //joueurs.ForEach(joueur => tasks.Add(LoadDetailJoueurFromSpid(joueur.Licence,true,false)));
                    joueurs.ForEach(joueur => tasks.Add(LoadJoueurFromSpid(joueur.Licence, false)));
                    await Task.WhenAll(tasks);

                    await DbContext.SaveChangesAsync();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
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

        private async Task<Joueur> LoadJoueurFromSpid(string licence,bool autoSave=true)
        {
            var uri = $"/api/joueur/{licence}/spid";
            
            JoueurSpid joueurspid= await this.InternalLoadFromSpid<ListeJoueurSpidHeader, JoueurSpidDto, JoueurSpid>(uri, false, liste => liste?.Liste?.Joueur, null, null, null);
            Joueur joueur =await LoadDetailJoueurFromSpid(joueurspid.Licence,false, false);
            if (joueur == null) return null;
            joueur.Extra = new JoueurExtra() { Licence = joueur.Licence };
            joueur.Sexe = joueurspid.Sexe;
            joueur.Type = joueurspid.Type;
            joueur.Certificat = joueurspid.Certificat;
            joueur.Validation = joueurspid.Validation;
            joueur.Echelon = joueurspid.Echelon;
            joueur.Place = joueurspid.Place;
            joueur.Mutation = joueurspid.Mutation;
            joueur.Arbitre = joueurspid.Arbitre;
            joueur.JugeArbitre = joueurspid.JugeArbitre;
            joueur.Tech = joueurspid.Tech;
            await this.DbContext.Set<Joueur>().AddAsync(joueur);
            if(autoSave)
                await DbContext.SaveChangesAsync();
            return joueur;
        }

        private async Task AddDetailJoueurFromspid()
        {

        }

        public Task<List<JoueurSearch>> ListeJoueursByClub(string club)
        {
            throw new NotImplementedException();
        }

        public async Task<List<JoueurSearch>> ListeJoueursByNom(string nom, string prenom)
        {
            return await loadJoueursByNom(nom, prenom);
        }

        public Task<List<JoueurSearch>> ListeJoueursByLicence(string licence)
        {
            throw new NotImplementedException();
        }
    }
}
