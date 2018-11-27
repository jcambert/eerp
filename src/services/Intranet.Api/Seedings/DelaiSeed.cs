using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;

namespace Intranet.Api.Seedings
{
    internal class DelaiSeed : ParametreSeed
    {
        public DelaiSeed(IntranetDbContext ctx, ApiSettings settings, IMapper mapper) : base(ctx, settings, mapper)
        {
        }

        public override string Type => Settings.Delai;

        public override int CodePrimaire => Settings.CodePrimaire.Delai;

        public override void Seed()
        {
            base.Seed();
            Add(0, 1, Mapper.Map<string>(new DelaiDto() { Delai = "A Convenir" }));
            Add(0, 2, Mapper.Map<string>(new DelaiDto() { Delai = "1 Semaine" }));
            Add(0, 3, Mapper.Map<string>(new DelaiDto() { Delai = "2 Semaines" }));
            Add(0, 4, Mapper.Map<string>(new DelaiDto() { Delai = "3 Semaines" }));
            Add(0, 5, Mapper.Map<string>(new DelaiDto() { Delai = "4 Semaines" }));
            Add(0, 6, Mapper.Map<string>(new DelaiDto() { Delai = "5 Semaines" }));
            Add(0, 7, Mapper.Map<string>(new DelaiDto() { Delai = "6 Semaines" }));
        }
    }
}
