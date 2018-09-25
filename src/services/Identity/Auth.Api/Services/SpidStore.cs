using Auth.Api.Data;
using Auth.Api.dto;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public abstract class AbstractSpidStore<TDbContext,TUser> : UserStore<PingUser> 
        where TUser : IdentityUser
        where TDbContext : IdentityDbContext<TUser>
    {
        public AbstractSpidStore(TDbContext context, SpidRequest<TDbContext> spidRequest,IConfiguration  configuration, IdentityErrorDescriber describer = null) : base(context, describer)
        {
            SpidRequest = spidRequest;
            Configuration = configuration;
        }

        public SpidRequest<TDbContext> SpidRequest { get; }
        public IConfiguration Configuration { get;  }
    }

    public class SpidStore : AbstractSpidStore<PingDbContext, PingUser>
    {
        public SpidStore(PingDbContext context, SpidRequest<PingDbContext> spidRequest, IConfiguration configuration, IdentityErrorDescriber describer = null) : base(context, spidRequest,configuration, describer)
        {
        }

        public new PingDbContext Context => base.Context as PingDbContext;

        private string SpidApiLicencePath => Configuration.GetValue<string>("ping:api:licence");

        public async Task<PingUser> FindByLicenceAsync(string licence, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await this.Context.Users.Where(pinguser => pinguser.Id == licence).FirstOrDefaultAsync();

            if (result == null)
            {
                var uri = SpidApiLicencePath.Replace("{licence}", licence);
                result=await SpidRequest.LoadFromSpid<ListeJoueurHeader, JoueurDto, PingUser>(uri, true, liste => liste.Liste.Licence, (db, user) => { db.Add<PingUser>(user); });
            }

            if (result == null)
            {
                ErrorDescriber.InvalidUserName(licence);
            }
            return result;

        }
    }
}
