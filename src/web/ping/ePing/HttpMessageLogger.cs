using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ePing { 


    public class HttpMessageLogger : DelegatingHandler
    {
        

        public HttpMessageLogger()
        {
            
        }
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var req =request.ToString();

            return await base.SendAsync(request, cancellationToken);
        }
    
    }
}
