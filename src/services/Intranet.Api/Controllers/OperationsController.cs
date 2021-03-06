﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Intranet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : BaseController
    {
        public OperationsController(IntranetDbContext context, IOptions<ApiSettings> settings, IMapper mapper) : base(context, settings, mapper)
        {
        }

        // GET: api/formats
        [HttpGet]
        public IEnumerable<OperationDto> GetOperations()
        {
            var opes = Context.Parametres.Where(p => p.CodePrimaire ==Settings.CodePrimaire.Operation && p.CodeSecondaire > 0).ToList();
            var result= Mapper.Map<List<OperationDto>>(opes);
            return result;

        }
    }
}