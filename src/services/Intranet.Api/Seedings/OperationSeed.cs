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
    internal class OperationSeed : ParametreSeed
    {
        public OperationSeed(IntranetDbContext ctx, ApiSettings settings, IMapper mapper) : base(ctx, settings, mapper)
        {
        }

        public override string Type => Settings.Operation;

        public override int CodePrimaire => Settings.CodePrimaire.Operation;

        public override void Seed()
        {
            base.Seed();
            Add(0, 1,  Mapper.Map<string>(new OperationDto() { Aide="Voir Calcul",Nom="Laser Bystronic",TauxHorraireOperation=110,TauxHorrairePreparation=70,TempsOperation=0,TempsPreparation=0.3 }));
            Add(0, 2, Mapper.Map<string>(new OperationDto() { Aide = "Voir Calcul", Nom = "T500 R2", TauxHorraireOperation = 95, TauxHorrairePreparation = 80, TempsOperation = 0, TempsPreparation = 0.5 }));

        }
    }
}
