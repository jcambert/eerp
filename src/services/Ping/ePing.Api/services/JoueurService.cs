using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.dto;
using ePing.Api.models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public interface IJoueurService
    {
        Task<List<Joueur>> loadListForClubFromSpid(string numero, bool addToDb,Club club);
        Task<Joueur> loadDetailJoueur(string licence, bool addToDb, Club club);
        Task<List<Partie>> loadJoueurParties(string licence);
    }

    public class JoueurService:ServiceBase,IJoueurService
    {
        public JoueurService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingDbContext dbcontext, EfService efService) : base(clientFactory, configuration, mapper, dbcontext,efService)
        {

        }

        public async Task<Joueur> loadDetailJoueur(string licence, bool addToDb, Club club)
        {
            return await this.InternalLoadFromSpid<ListeJoueurHeader, JoueurDto, Joueur>($"api/joueur/{licence}", addToDb, liste => liste?.Liste?.Joueur, (ctx, joueur) => { ctx.Joueur.Add(joueur); }, joueur => joueur.Club = club);
        }

        public async Task<List<Joueur>> loadListForClubFromSpid(string numero, bool addToDb,Club club)
        {
            return await this.InternalLoadListFromSpid<ListeJoueursHeader, List<JoueurDto>, Joueur>($"api/joueur/liste/club/{numero}", addToDb, liste => liste?.Liste?.Joueurs, (ctx, model) => { ctx.Joueur.AddRange(model); },model=> model.Club=club );
        }

        public async Task<List<Partie>> loadJoueurParties(string licence)
            {
            return await this.InternalLoadListFromSpid<ListePartiesHeader, List<PartieDto>, Partie>($"api/joueur/{licence}/parties", false, liste => liste?.Liste?.Parties);
        }
    }
}
