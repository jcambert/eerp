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

    public interface IClubService
    {
        Task<Club> loadFromSpid(string numero, bool addIfnotExist);
    }
    public class ClubService:ServiceBase ,IClubService
    {
   

        public ClubService(IHttpClientFactory clientFactory, IConfiguration configuration,IMapper mapper,PingContext dbcontext):base(clientFactory,configuration,mapper,dbcontext)
        {
   
        }
        public async Task<Club> loadFromSpid(string numero, bool addToDb)
        {
            return await this.InternalLoadFromSpid<ListeClubHeader,ClubDto, Club>($"/api/club/numero/{numero}", true, liste => liste.Liste.Club, (ctx, model) => { ctx.Clubs.Add(model); });
        }

  
    }
}
