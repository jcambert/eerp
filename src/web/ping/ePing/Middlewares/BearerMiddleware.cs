using ePing.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing
{
    

    public class BearerMiddleware
    {
        private readonly RequestDelegate _next;

        public BearerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var auth = httpContext.Session.GetValue<string>("auth");
            if (auth != null)
            {
                var bearer = JsonConvert.DeserializeObject<BearerDto>(auth);
                httpContext.Request.Headers.Add("Authorization", $"Bearer { bearer.Jwt }");
            }
            await _next.Invoke(httpContext);

           
        }
    }

    public static class BearerMiddlewareMiddlewareExtensions
    {
        public static IApplicationBuilder UseBearerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BearerMiddleware>();
        }
    }
}
