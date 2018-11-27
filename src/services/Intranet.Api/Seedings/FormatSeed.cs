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
    internal class FormatSeed : ParametreSeed
    {
        public FormatSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }

        public override string Type => Settings.Format;

        public override int CodePrimaire => Settings.CodePrimaire.Format;

        public override void Seed()
        {
            base.Seed();
           /* Add(0, 1, "2000x1000");
            Add(0, 2, "2500x1250");
            Add(0, 3, "3000x1500");*/
            Add(0, 1, Mapper.Map<string>(new FormatDto() { Longueur = 2000, Largeur = 1000 }));
            Add(0, 2, Mapper.Map<string>(new FormatDto() { Longueur = 2500, Largeur = 1250 }));
            Add(0, 3, Mapper.Map<string>(new FormatDto() { Longueur = 3000, Largeur = 1500 }));
        }
    }
}
