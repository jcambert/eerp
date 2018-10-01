using Auth.Api.Data;
using Auth.Api.dto;
using Auth.Api.Models;
using Auth.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Api.Repositories
{
    public class JoueurRepository
    {
        public JoueurRepository(PingDbContext context, SpidRequest<PingDbContext> spidRequest, IConfiguration configuration)
        {
            PingDbContext = context;

            SpidRequest = spidRequest;
            Configuration = configuration;
        }

        public PingDbContext PingDbContext { get;  }
        public SpidRequest<PingDbContext> SpidRequest { get; }
        public IConfiguration Configuration { get; }

        private string SpidApiLicencePath => Configuration.GetValue<string>("ping:api:licence");

        public async Task<PingUser> FindByLicenceAsync(string licence, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await this.PingDbContext.Users.Where(pinguser => pinguser.Id == licence).FirstOrDefaultAsync();

            if (result == null)
            {
                var uri = SpidApiLicencePath.Replace("{licence}", licence);
                result =  await SpidRequest.LoadFromSpid<ListeJoueurHeader, JoueurDto, PingUser>(uri, true, liste => liste.Liste.Licence, (db, user) => { db.Add<PingUser>(user); });
            }


            return result;

        }
    }
}
