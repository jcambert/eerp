using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.Controllers
{
    public class BaseController:ControllerBase
    {
        public BaseController(IntranetDbContext context, IOptions<ApiSettings> settings, IMapper mapper)
        {
            Context = context;
            Settings = settings.Value;
            Mapper = mapper;
        }

        public IntranetDbContext Context { get; }
        public ApiSettings Settings { get; }
        public IMapper Mapper { get; }
    }
}
