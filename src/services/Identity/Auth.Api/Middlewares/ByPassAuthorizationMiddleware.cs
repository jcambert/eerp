using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Api.Middlewares
{
    public class ByPassAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public ByPassAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, ""),
                            }, "");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.User = claimsPrincipal;



            await _next(context);
        }
    }
}
