using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("X-Xss-Protection", "1");
            httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            await _next.Invoke(httpContext);

            if (httpContext.Response.StatusCode == 401)
            {

                 httpContext.Response.Redirect("/Login");// await WriteAsync("You are not authorized to access this page");
            }
        }
    }

    public static class UnauthorizedMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnauthorizedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnauthorizedMiddleware>();
        }
    }
}
