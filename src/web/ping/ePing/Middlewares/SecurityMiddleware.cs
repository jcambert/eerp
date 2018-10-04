using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
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
                var r = httpContext.Request;
            }
        }
    }

    public static class SecurityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityMiddleware>();
        }
    }
}
