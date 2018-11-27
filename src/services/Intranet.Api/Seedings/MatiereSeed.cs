using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;

namespace Intranet.Api.Seedings
{
    internal class MatiereSeed : ParametreSeed
    {
        public MatiereSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }

        public override string Type => Settings.Matiere;

        public override int CodePrimaire => Settings.CodePrimaire.Matiere;

        public override void Seed()
        {
           
            Add(0, 1, Mapper.Map<string>(new MatiereDto() { Matiere = "Xc10<3", PrixKg= 0.78 }));
            Add(0, 2, Mapper.Map<string>(new MatiereDto() { Matiere = "Xc10>3", PrixKg = 0.71 }));

            Add(1, 1, Mapper.Map<string>(new MatiereDto() { Matiere = "304L", PrixKg = 2.85 }));
            Add(1, 2, Mapper.Map<string>(new MatiereDto() { Matiere = "304L PVC", PrixKg = 2.9 }));

            Add(2, 1, Mapper.Map<string>(new MatiereDto() { Matiere = "AG3 5754", PrixKg = 3.25 }));
            Add(2, 2, Mapper.Map<string>(new MatiereDto() { Matiere = "AG3 5754 PVC", PrixKg = 3.35 }));
        }
    }
}
