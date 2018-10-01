using Auth.Api.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Api
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private JoueurRepository _repo;

        public ResourceOwnerPasswordValidator(JoueurRepository repo)
        {
            _repo = repo;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user =  await _repo.FindByLicenceAsync(context.UserName);
            if (user != null)
                context.Result = new GrantValidationResult(context.UserName, "password", null, "local", null);
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant) ;
            }
           
        }
    }

    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = new List<Claim>();
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
