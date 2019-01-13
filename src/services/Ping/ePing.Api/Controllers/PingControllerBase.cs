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
        public PingControllerBase(/*PingDbContext context*/)
        {
           // Context = context;
        }

       // public PingDbContext Context { get;  }
    }
}
