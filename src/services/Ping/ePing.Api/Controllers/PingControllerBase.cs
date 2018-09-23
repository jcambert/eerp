using ePing.Api.dbcontext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.Controllers
{
    public class PingControllerBase:ControllerBase
    {
        public PingControllerBase(PingContext context)
        {
            Context = context;
        }

        public PingContext Context { get;  }
    }
}
