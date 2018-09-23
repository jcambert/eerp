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
    }

    public class JoueurService:ServiceBase,IJoueurService
    {
        public JoueurService(IHttpClientFactory clientFactory, IConfiguration configuration, IMapper mapper, PingContext dbcontext) : base(clientFactory, configuration, mapper, dbcontext)
        {

        }

        public async Task<List<Joueur>> loadListForClubFromSpid(string numero, bool addToDb,Club club)
        {
            return await this.InternalLoadFromSpid<ListeJoueurHeader, List<JoueurDto>, List<Joueur>>($"api/joueur/liste/club/{numero}", true, liste => liste.Liste.Joueurs, (ctx, model) => { ctx.Joueur.AddRange(model); },models=>  models.ForEach(model=> model.Club=club ) );
        }
    }
}
