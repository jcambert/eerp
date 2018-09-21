using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ping.Api.services;

namespace Ping.Api.Controllers
{

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