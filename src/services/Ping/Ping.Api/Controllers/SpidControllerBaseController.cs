using Microsoft.AspNetCore.Mvc;
using Ping.Api.services;

namespace Ping.Api.Controllers
{
    //[Authorize]
    public abstract class SpidControllerBase : ControllerBase
    {

        public SpidControllerBase(ISpidRequest request, ISpidConfiguration configuration)
        {
            SpidRequest = request;
            Configuration = configuration;

        }

        public ISpidRequest SpidRequest { get; }

        public ISpidConfiguration Configuration { get; }

    }
}