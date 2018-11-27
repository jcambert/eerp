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
    internal class TraitementSeed : ParametreSeed
    {
        public TraitementSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }

        public override string Type => Settings.TraitementDeSurface;

        public override int CodePrimaire => Settings.CodePrimaire.TraitementDeSurface;

        public override void Seed()
        {
            base.Seed();
            Add(0, 1, Mapper.Map<string>(new TraitementDto() { Traitement = "Brut" }));
            Add(0, 2, Mapper.Map<string>(new TraitementDto() { Traitement = "Thermolaquage" }));
            Add(0, 3, Mapper.Map<string>(new TraitementDto() { Traitement = "Haute Temperature" }));
        }
    }
}
