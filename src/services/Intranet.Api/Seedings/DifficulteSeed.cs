using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;

namespace Intranet.Api.Seedings
{
    internal class DifficulteSeed : ParametreSeed
    {
        public DifficulteSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }

        public override string Type => Settings.Difficulte;

        public override int CodePrimaire => Settings.CodePrimaire.Difficulte;

        public override void Seed()
        {
            base.Seed();
            Add(0, 1, Mapper.Map<string>(new DifficulteDto() { Difficulte = "Facile" }));
            Add(0, 2, Mapper.Map<string>(new DifficulteDto() { Difficulte = "Moyen" }));
            Add(0, 3, Mapper.Map<string>(new DifficulteDto() { Difficulte = "Difficile" }));
        }
    }
}
