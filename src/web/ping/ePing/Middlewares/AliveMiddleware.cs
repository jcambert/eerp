using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ePing
{
    public class AliveMiddleware
    {

        private readonly RequestDelegate _next;

        public AliveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/alive")
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync("ok");
            }
            else
                await _next.Invoke(httpContext);
        }
    }

    public static class AliveMiddlewareExtensions
    {
        public static IApplicationBuilder UseAliveMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AliveMiddleware>();
        }
    }
}
