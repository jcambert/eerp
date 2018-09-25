using Auth.Api.Data;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Services
{
    public class SpidUserManager : UserManager<PingUser>
    {
        public SpidUserManager(IUserStore<PingUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<PingUser> passwordHasher, IEnumerable<IUserValidator<PingUser>> userValidators, IEnumerable<IPasswordValidator<PingUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<PingUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
           
        }

        public new SpidStore Store => base.Store as SpidStore;

       

        public async Task<PingUser> FindByLicenceAsync(string licence)
        {
            //var result = base.FindByIdAsync(licence);
            var result =await Store.FindByLicenceAsync(licence);
            return result;
            //throw new NotImplementedException();
        }
    }
}
