using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ping.Api.logging
{


    public class HttpMessageLogger : DelegatingHandler
    {
        private readonly ILogger _logger;

        public HttpMessageLogger(ILoggerFactory loggerFactory)
        {
            _logger =loggerFactory.CreateLogger("Ping.Api");
        }
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug(request.ToString());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
