using Auth.Api.Data;
using Auth.Api.dto;
using Auth.Api.Models;
using Auth.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public PingDbContext PingDbContext { get; }
        public SpidRequest<PingDbContext> SpidRequest { get; }
        public IConfiguration Configuration { get; }

        private string SpidApiLicencePath(string licence)
        {
            var path = Configuration.GetValue<string>("ping:api:licence");
            return path.Replace("{licence}", licence);
        }

        private string SpidApiNomPrenomPath(string nom, string prenom)
        {
            var path = Configuration.GetValue<string>("ping:api:nomprenom");
            return path.Replace("{nom}", nom).Replace("{/prenom}", $"/{prenom ?? ""}" );
        }

        public async Task<PingUser> FindByLicenceAsync(string licence, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await this.PingDbContext.Users.Where(pinguser => pinguser.Id == licence).FirstOrDefaultAsync();

            if (result == null)
            {
                var uri = SpidApiLicencePath(licence);
                result = await SpidRequest.LoadFromSpid<ListeJoueurHeader, JoueurDto, PingUser>(uri, true, liste => liste.Liste.Licence, (db, user) => { db.Add<PingUser>(user); });
            }


            return result;

        }

        public async Task<PingUser> FindByNameAsync(string nom, string prenom , CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await this.PingDbContext.Users.Where(pinguser => pinguser.Nom == nom &&  pinguser.Prenom == prenom).FirstOrDefaultAsync();

            if (result == null)
            {
                var uri = SpidApiNomPrenomPath(nom,prenom);
                result = await SpidRequest.LoadFromSpid<ListeJoueurHeader, JoueurDto, PingUser>(uri, true, liste => liste.Liste.Joueur, (db, user) => { db.Add<PingUser>(user); });
            }


            return result;

        }
    }
}
