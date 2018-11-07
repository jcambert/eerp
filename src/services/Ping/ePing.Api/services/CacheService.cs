using ePing.Api.dbcontext;
using ePing.Api.models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ePing.Api.services
{
    public class CacheService
    {
        public CacheService(PingDbContext ctx,ChampionnatService service)
        {
            Context = ctx;
            ChampionnatService = service;
        }

        public PingDbContext Context { get; }
        public ChampionnatService ChampionnatService { get; }

        private bool GetClassementLimit(Equipe equipe)
        {
            var cache = Context.Cache.Where(c => c.Equipe == equipe).FirstOrDefault();
            if (cache == null) return false;
                
            return cache.ClassementLimit<=DateTime.Now ;
        }

        private async Task  SetClassementLimit(Equipe equipe)
        {
            var cache = Context.Cache.Where(c => c.Equipe == equipe).FirstOrDefault();
            var resultats = await ChampionnatService.LoadResultats(equipe);
            var date = resultats.Where(r => r.DatePrevue >= DateTime.Now).FirstOrDefault()?.DatePrevue;
            if (date == null) date = DateTime.Now.AddDays(1);
            if (cache == null)
            {
                
                Cache _cache = new Cache() { Equipe = equipe, ClassementLimit = date };
                Context.Cache.Add(_cache);
                
            }
            else
            {
                cache.ClassementLimit = date;

            }
        }

        public async Task<List<ClassementEquipe>> GetClassements(Equipe equipe)
        {
            if (GetClassementLimit(equipe))
                return Context.ClassementsEquipes.Where(c => c.LibelleEquipe == equipe.Nom).ToList();
            var result = await ChampionnatService.LoadClassements(equipe);
            Context.ClassementsEquipes.RemoveRange(result);
            await Context.ClassementsEquipes.AddRangeAsync(result);
            await SetClassementLimit(equipe);
            await Context.SaveChangesAsync();
            return result;
        }
    }
}
